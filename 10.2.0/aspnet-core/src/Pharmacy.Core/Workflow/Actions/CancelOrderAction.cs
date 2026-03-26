using Pharmacy.Authorization;
using Pharmacy.Orders;

namespace Pharmacy.Workflow.Actions
{
    public class CancelOrderAction : IOrderAction
    {
        public string      Name               => "CancelOrder";
        public OrderStatus FromStatus         => OrderStatus.Submitted;
        public OrderStatus ToStatus           => OrderStatus.Cancelled;
        public string      PermissionRequired => PermissionNames.Orders_Cancel;
    }
}