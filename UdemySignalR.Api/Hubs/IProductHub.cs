using System.Threading.Tasks;
using UdemySignalR.Api.Models;

namespace UdemySignalR.Api.Hubs
{
    public interface IProductHub
    {
        Task ReceiveProduct(Product p);
    }
}
