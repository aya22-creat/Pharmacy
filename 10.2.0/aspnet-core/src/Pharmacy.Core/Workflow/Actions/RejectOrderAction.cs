using Pharmacy.Authorization;
using Pharmacy.Orders;

namespace Pharmacy.Workflow.Actions
{
    public class RejectOrderAction : IOrderAction
    {
        public string      Name               => "RejectOrder";
        public OrderStatus FromStatus         => OrderStatus.UnderReview;
        public OrderStatus ToStatus           => OrderStatus.Rejected;
        public string      PermissionRequired => PermissionNames.Orders_Reject;
    }
}