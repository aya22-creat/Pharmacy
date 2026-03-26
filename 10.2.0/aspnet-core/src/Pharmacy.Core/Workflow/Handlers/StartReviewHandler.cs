using Pharmacy.Orders;
using System.Threading.Tasks;

namespace Pharmacy.Workflow.Handlers
{
    public class StartReviewHandler : IOrderActionHandler
    {
        public string ActionName => "StartReview";

        public Task HandleAsync(Order order, string notes, long userId)
        {
            order.ApplyStatusChange(OrderStatus.UnderReview, notes, userId);
            return Task.CompletedTask;
        }
    }
}