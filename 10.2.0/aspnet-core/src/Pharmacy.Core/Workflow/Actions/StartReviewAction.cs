using Pharmacy.Authorization;
using Pharmacy.Orders;

namespace Pharmacy.Workflow.Actions
{
    public class StartReviewAction : IOrderAction
    {
        public string      Name               => "StartReview";
        public OrderStatus FromStatus         => OrderStatus.Submitted;
        public OrderStatus ToStatus           => OrderStatus.UnderReview;
        public string      PermissionRequired => PermissionNames.Orders_StartReview;
    }
}