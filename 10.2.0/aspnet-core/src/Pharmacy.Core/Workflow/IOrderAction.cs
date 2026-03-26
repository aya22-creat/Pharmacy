using Pharmacy.Orders;

namespace Pharmacy.Workflow
{
    public interface IOrderAction
    {
        string      Name               { get; }
        OrderStatus FromStatus         { get; }
        OrderStatus ToStatus           { get; }
        string      PermissionRequired { get; }
    }
}