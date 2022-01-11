using System.Collections.Generic;

namespace UdemySignalR.Api.Models
{
    public class Team
    {
        // team üzerinden user eklerken öncelikle userın nesne örneği ctorda almalıyız
        public Team()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
