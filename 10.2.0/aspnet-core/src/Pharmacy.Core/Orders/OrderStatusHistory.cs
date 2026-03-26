using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Orders;

public class OrderStatusHistory : CreationAuditedEntity<int>
{
    public int OrderId { get; private set; }
    public OrderStatus FromStatus { get; private set; }
    public OrderStatus ToStatus { get; private set; }
    public string ActionName { get; private set; }
    public string Notes { get; private set; } 
    public long  ActorUserId { get; private set; }

protected OrderStatusHistory(){}

public OrderStatusHistory(
    int orderId,
    OrderStatus fromStatus,
    OrderStatus toStatus,
    string actionName,
    string notes,
    long actorUserId
)
{
    OrderId = orderId;
    FromStatus = fromStatus;
    ToStatus = toStatus;
    ActionName = actionName;
    Notes = notes;
    ActorUserId = actorUserId;
    CreationTime = Clock.Now;
}
}