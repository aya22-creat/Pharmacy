using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmacy.Orders
{
    public class Order : FullAuditedEntity<int>
    {
        public long CustomerId { get; private set; }
        public int? PrescriptionId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal CoveredAmount { get; private set; }
        public decimal PatientAmount { get; private set; }

        private readonly List<OrderItem> _items = new();
        private readonly List<OrderStatusHistory> _statusHistories = new();
        private readonly List<object> _domainEvents = new();

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<OrderStatusHistory> StatusHistories => _statusHistories.AsReadOnly();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();
        private void AddDomainEvent(object domainEvent) => _domainEvents.Add(domainEvent);

        protected Order() { }

        public Order(long customerId, int? prescriptionId = null)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");

            CustomerId = customerId;
            PrescriptionId = prescriptionId;
            Status = OrderStatus.Submitted;
        }

    
        public void AddItem(int productId, string productName, int quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.Submitted)
                throw new InvalidOperationException("Cannot add items after review starts.");

            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (unitPrice <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (_items.Any(i => i.ProductId == productId))
                throw new InvalidOperationException("Product already added.");

            var item = new OrderItem(productId, productName, quantity, unitPrice);
            _items.Add(item);

            RecalculateTotals();
        }

  
        public void StartReview(long userId)
        {
            if (Status != OrderStatus.Submitted)
                throw new InvalidOperationException("Only submitted orders can start review.");

            if (!_items.Any())
                throw new InvalidOperationException("Order must contain at least one item.");

            ApplyStatusChange(OrderStatus.UnderReview, "Review started", userId);
        }

        public void ReviewItem(int productId, ReviewDecision decision,
            decimal coveredAmount = 0, string notes = null)
        {
            if (Status != OrderStatus.UnderReview)
                throw new InvalidOperationException("Order must be UnderReview.");

            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Item not found.");

            if (item.Decision != ReviewDecision.Pending)
                throw new InvalidOperationException("Item already reviewed.");

            if (decision == ReviewDecision.FullyApproved)
            {
                item.ReviewAsFullyApproved(notes);
            }
            else if (decision == ReviewDecision.PartiallyApproved)
            {
                if (coveredAmount <= 0 || coveredAmount > item.TotalPrice)
                    throw new InvalidOperationException("Invalid covered amount.");

                item.ReviewAsPartiallyApproved(coveredAmount, notes);
            }
            else if (decision == ReviewDecision.Rejected)
            {
                item.ReviewAsRejected(notes);
            }
            else
            {
                throw new InvalidOperationException("Invalid review decision.");
            }

            RecalculateTotals();
        }

      
        public void ApplyStatusChange(OrderStatus newStatus, string notes, long userId)
        {
            if (Status == newStatus)
                throw new InvalidOperationException("Already in this status.");

            if (Status.IsTerminal)
                throw new InvalidOperationException("Order already completed.");

            if (!Status.CanTransitionTo(newStatus))
                throw new InvalidOperationException($"Invalid transition {Status.Name} → {newStatus.Name}");

            var oldStatus = Status;

            _statusHistories.Add(new OrderStatusHistory(
                Id,
                oldStatus,
                newStatus,
                $"{oldStatus.Name}→{newStatus.Name}",
                notes,
                userId
            ));

            Status = newStatus;
            AddDomainEvent(new Events.OrderStatusChangedEvent(
                Id,
                oldStatus,
                newStatus,
                userId,
                notes
            ));
        }

        public void Complete(long userId)
        {
            if (Status != OrderStatus.Approved && Status != OrderStatus.PartiallyApproved)
                throw new InvalidOperationException("Only approved orders can be completed.");

            ApplyStatusChange(OrderStatus.Completed, "Order completed", userId);
        }

    
        public bool IsFullyReviewed()
        {
            return _items.Any() && _items.All(i => i.Decision != ReviewDecision.Pending);
        }

        public bool HasPartialApproval()
        {
            if (!_items.Any()) return false;

            var reviewed = _items.Where(i => i.Decision != ReviewDecision.Pending).ToList();
            if (!reviewed.Any()) return false;

            return reviewed.Any(i => i.Decision == ReviewDecision.PartiallyApproved);
        }

        public bool HasMixedApproval()
        {
            if (!_items.Any()) return false;

            var reviewed = _items.Where(i => i.Decision != ReviewDecision.Pending).ToList();
            return reviewed.Any(i => i.IsApproved) && reviewed.Any(i => i.IsRejected);
        }

        private void RecalculateTotals()
        {
            TotalAmount = _items.Sum(i => i.TotalPrice);
            CoveredAmount = _items.Sum(i => i.CoveredAmount);
            PatientAmount = _items.Sum(i => i.PatientAmount);
        }
    }
}