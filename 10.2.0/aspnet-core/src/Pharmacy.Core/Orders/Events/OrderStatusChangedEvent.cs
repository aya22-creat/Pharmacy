namespace Pharmacy.Orders.Events
{
    public sealed class OrderStatusChangedEvent
    {
        public int OrderId { get; }
        public OrderStatus OldStatus { get; }
        public OrderStatus NewStatus { get; }
        public long UserId { get; }
        public string Notes { get; }

        public OrderStatusChangedEvent(int orderId, OrderStatus oldStatus, OrderStatus newStatus, long userId, string notes)
        {
            OrderId = orderId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            UserId = userId;
            Notes = notes;
        }
    }
}
