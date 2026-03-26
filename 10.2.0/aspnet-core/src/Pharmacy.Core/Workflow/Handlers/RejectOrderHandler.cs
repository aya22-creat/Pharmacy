using Pharmacy.Orders;
using System.Threading.Tasks;

namespace Pharmacy.Workflow.Handlers
{
    public class RejectOrderHandler : IOrderActionHandler
    {
        public string ActionName => "RejectOrder";

        public Task HandleAsync(Order order, string notes, long userId)
        {
            order.ApplyStatusChange(OrderStatus.Rejected, notes, userId);
            return Task.CompletedTask;
        }
    }
}