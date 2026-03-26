using Pharmacy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmacy.Orders
{
    public class OrderStatus : Enumeration
    {
        public static readonly OrderStatus Submitted          = new(1, "Submitted");
        public static readonly OrderStatus UnderReview        = new(2, "UnderReview");
        public static readonly OrderStatus Approved           = new(3, "Approved");
        public static readonly OrderStatus PartiallyApproved  = new(4, "PartiallyApproved");
        public static readonly OrderStatus Rejected           = new(5, "Rejected");
        public static readonly OrderStatus Cancelled          = new(6, "Cancelled");
        public static readonly OrderStatus Completed          = new(7, "Completed");

        private static readonly Dictionary<int, List<int>> _transitions = new()
        {
            {
                Submitted.Id,
                new() { UnderReview.Id, Cancelled.Id }
            },
            {
                UnderReview.Id,
                new() { Approved.Id, PartiallyApproved.Id, Rejected.Id, Cancelled.Id }
            },
            {
                Approved.Id,
                new() { Completed.Id }
            },
            {
                PartiallyApproved.Id,
                new() { Completed.Id }
            },
        };

        protected OrderStatus(int id, string name) : base(id, name) { }

        public static OrderStatus From(int id)
        {
            var status = GetAll<OrderStatus>().SingleOrDefault(s => s.Id == id);
            if (status == null)
                throw new InvalidOperationException($"Invalid OrderStatus id '{id}'.");
            return status;
        }

        public bool CanTransitionTo(OrderStatus target)
        {
            return _transitions.TryGetValue(Id, out var allowed)
                   && allowed.Contains(target.Id);
        }

        public bool IsTerminal =>
            this == Approved          ||
            this == PartiallyApproved ||
            this == Rejected          ||
            this == Cancelled        ||
            this == Completed;
    }
}