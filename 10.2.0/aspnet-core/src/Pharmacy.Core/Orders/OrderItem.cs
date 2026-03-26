using Abp.Domain.Entities;
using System;

namespace Pharmacy.Orders
{
    public class OrderItem : Entity<int>
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal CoveredAmount { get; private set; }
        public decimal PatientAmount { get; private set; }
        public ReviewDecision Decision { get; private set; }
        public string ReviewNotes { get; private set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        protected OrderItem() { }

        public OrderItem(int productId, string productName, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            Decision = ReviewDecision.Pending;

            SetAmounts(0);
        }

      
        public void ReviewAsFullyApproved(string notes = null)
        {
            EnsureNotReviewed();

            SetAmounts(TotalPrice);
            Decision = ReviewDecision.FullyApproved;
            ReviewNotes = notes;
        }

        public void ReviewAsPartiallyApproved(decimal coveredAmount, string notes = null)
        {
            EnsureNotReviewed();

            if (coveredAmount <= 0 || coveredAmount > TotalPrice)
                throw new ArgumentException("Invalid covered amount.");

            SetAmounts(coveredAmount);
            Decision = ReviewDecision.PartiallyApproved;
            ReviewNotes = notes;
        }

        public void ReviewAsRejected(string notes = null)
        {
            EnsureNotReviewed();

            SetAmounts(0);
            Decision = ReviewDecision.Rejected;
            ReviewNotes = notes;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = quantity;

            if (Decision == ReviewDecision.FullyApproved)
                SetAmounts(TotalPrice);
            else if (Decision == ReviewDecision.Rejected)
                SetAmounts(0);
            else if (Decision == ReviewDecision.PartiallyApproved)
                SetAmounts(CoveredAmount);
        }

        private void SetAmounts(decimal covered)
        {
            CoveredAmount = covered;
            PatientAmount = TotalPrice - covered;
        }

        private void EnsureNotReviewed()
        {
            if (Decision != ReviewDecision.Pending)
                throw new InvalidOperationException("Item already reviewed.");
        }

     
        public bool IsApproved =>
            Decision == ReviewDecision.FullyApproved ||
            Decision == ReviewDecision.PartiallyApproved;

        public bool IsRejected => Decision == ReviewDecision.Rejected;
        public bool IsPending => Decision == ReviewDecision.Pending;
    }
}