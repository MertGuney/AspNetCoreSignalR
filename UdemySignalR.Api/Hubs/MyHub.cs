using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemySignalR.Api.Models;

namespace UdemySignalR.Api.Hubs
{
    public class MyHub : Hub
    {
        private readonly AppDbContext _context;
        public MyHub(AppDbContext context)
        {
            _context = context;
        }

        private static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;
        public static int TeamCount { get; set; } = 7;

        public async Task SendProduct(Product p)
        {
            await Clients.All.SendAsync("ReceiveProduct", p);
        }
        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                await Clients.Caller.SendAsync("Error", $"Takım en fazla {TeamCount} kişi olabilir.");
            }
            else
            {
                Names.Add(name);
                //Eğer clientlar ReceiveName olarak tanımladığımız method adını tanımlayıp subcribe olduysa message'ı alıcak.
                await Clients.All.SendAsync("ReceiveName", name);//All prop->bu huba bağlı olan bütün clientlara bildiri gönderir.
            }
        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        //Groups
        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }

        public async Task SendNameByGroup(string name, string teamName)
        {
            var team = _context.Teams.Where(x => x.Name == teamName).FirstOrDefault();
            if (team != null)
            {
                // team üzerinden user ekleme işlemini kullanıyoruz.
                team.Users.Add(new User { Name = name });
            }
            else
            {
                var newTeam = new Team { Name = name };
                newTeam.Users.Add(new User { Name = name });
                _context.Teams.Add(newTeam);
            }
            await _context.SaveChangesAsync();

            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup", name, team.Id);
        }

        public async Task GetNamesByGroup()
        {
            var teams = _context.Teams.Include(x => x.Users).Select(x => new
            {
                teamId = x.Id,
                users = x.Users.ToList()
            });
            await Clients.All.SendAsync("ReceiveNamesByGroup", teams);
        }

        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }


        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
