// Pharmacy.Core/Orders/ReviewDecision.cs
using Pharmacy.Shared;
using System;
using System.Linq;

namespace Pharmacy.Orders
{
    public class ReviewDecision : Enumeration
    {
        public static readonly ReviewDecision Pending           = new(1, "Pending");
        public static readonly ReviewDecision FullyApproved    = new(2, "FullyApproved");
        public static readonly ReviewDecision PartiallyApproved = new(3, "PartiallyApproved");
        public static readonly ReviewDecision Rejected          = new(4, "Rejected");

        protected ReviewDecision(int id, string name) : base(id, name) { }

        public static ReviewDecision From(int id)
        {
            var decision = GetAll<ReviewDecision>().SingleOrDefault(s => s.Id == id);
            if (decision == null)
                throw new InvalidOperationException(
                    $"Invalid ReviewDecision id '{id}'.");
            return decision;
        }
    }
}