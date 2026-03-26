using Pharmacy.Orders;
using System.Threading.Tasks;

namespace Pharmacy.Workflow.Handlers
{
    public class FinalizeReviewHandler : IOrderActionHandler
    {
        public string ActionName => "FinalizeReview";

        public Task HandleAsync(Order order, string notes, long userId)
        {
            var status = order.HasPartialApproval()
                ? OrderStatus.PartiallyApproved
                : OrderStatus.Approved;

            order.ApplyStatusChange(status, notes, userId);
            return Task.CompletedTask;
        }
    }
}