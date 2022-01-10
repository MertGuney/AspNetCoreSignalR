using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UdemySignalR.Api.Hubs
{
    public class MyHub : Hub
    {
        public static List<string> Names { get; set; } = new List<string>();
        public async Task SendName(string name)
        {
            Names.Add(name);
            //Eğer clientlar ReceiveName olarak tanımladığımız method adını tanımlayıp subcribe olduysa message'ı alıcak.
            await Clients.All.SendAsync("ReceiveName", name);//All prop->bu huba bağlı olan bütün clientlara bildiri gönderir.
        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }
    }
}
