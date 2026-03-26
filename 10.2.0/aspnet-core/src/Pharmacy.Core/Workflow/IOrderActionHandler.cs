using Pharmacy.Orders;
using System.Threading.Tasks;

namespace Pharmacy.Workflow
{
    public interface IOrderActionHandler
    {
        string ActionName { get; }
        Task HandleAsync(Order order, string notes, long userId);
    }
}