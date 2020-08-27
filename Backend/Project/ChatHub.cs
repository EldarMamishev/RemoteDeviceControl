using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApi
{
    public class ChatHub : Hub
    {
        public Task SendToAll(string name, string message)
        {
            return Clients.All.SendAsync("sendToAll", name, message);
        }

        public ChatHub()
        {
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}