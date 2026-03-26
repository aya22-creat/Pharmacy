using Pharmacy.Authorization;
using Pharmacy.Orders;

namespace Pharmacy.Workflow.Actions
{
    public class FinalizeReviewAction : IOrderAction
    {
        public string      Name               => "FinalizeReview";
        public OrderStatus FromStatus         => OrderStatus.UnderReview;
        public OrderStatus ToStatus           => OrderStatus.Approved;
        public string      PermissionRequired => PermissionNames.Orders_FinalizeReview;
    }
}