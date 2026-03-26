using Pharmacy.Orders;
using System.Threading.Tasks;

namespace Pharmacy.Workflow.Handlers
{
    public class CancelOrderHandler : IOrderActionHandler
    {
        public string ActionName => "CancelOrder";

        public Task HandleAsync(Order order, string notes, long userId)
        {
            order.ApplyStatusChange(OrderStatus.Cancelled, notes, userId);
            return Task.CompletedTask;
        }
    }
}