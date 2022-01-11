using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using UdemySignalR.Api.Models;

namespace UdemySignalR.Api.Hubs
{
    public class ProductHub : Hub<IProductHub>
    {
        public async Task SendProduct(Product p)
        {
            await Clients.All.ReceiveProduct(p);
        }
    }
}
