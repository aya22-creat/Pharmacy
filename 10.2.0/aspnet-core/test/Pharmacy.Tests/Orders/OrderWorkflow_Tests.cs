using Pharmacy.Orders;
using Pharmacy.Workflow.Handlers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pharmacy.Tests.Orders
{
    public class OrderWorkflow_Tests
    {
        [Fact]
        public async Task OrderWorkflow_TransitionsAndHistory_AreCorrect()
        {
            // Arrange
            var order = new Order(customerId: 1);
            order.Items.Count.ShouldBe(0);
            order.Status.ShouldBe(OrderStatus.Submitted);

            // Act
            var start = new StartReviewHandler();
            await start.HandleAsync(order, "start review", userId: 123);

            // Assert
            order.Status.ShouldBe(OrderStatus.UnderReview);
            order.StatusHistories.Count.ShouldBe(1);
            order.StatusHistories.ShouldContain(h => h.FromStatus == OrderStatus.Submitted && h.ToStatus == OrderStatus.UnderReview);

            // Act finalize
            var finalize = new FinalizeReviewHandler();
            await finalize.HandleAsync(order, "finalize", userId: 123);

            // Assert final status
            order.Status.ShouldBeOneOf(OrderStatus.Approved, OrderStatus.PartiallyApproved);
            order.StatusHistories.Count.ShouldBe(2);

            // Act reject from terminal should throw
            var reject = new RejectOrderHandler();
            await Should.ThrowAsync<InvalidOperationException>(() => reject.HandleAsync(order, "reject", userId: 123));
        }

        [Fact]
        public async Task CancelOrderHandler_StopsAtSubmittedOrUnderReview()
        {
            var order = new Order(customerId: 1);

            var cancel = new CancelOrderHandler();
            await cancel.HandleAsync(order, "cancel", userId: 1);
            order.Status.ShouldBe(OrderStatus.Cancelled);
            order.StatusHistories.Count.ShouldBe(1);

            // cannot cancel terminal
            await Should.ThrowAsync<InvalidOperationException>(() => cancel.HandleAsync(order, "cancel2", userId: 1));
        }
    }
}
