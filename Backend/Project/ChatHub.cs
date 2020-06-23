using Microsoft.AspNetCore.SignalR;

namespace WebApi
{
    public class ChatHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
        }

        public ChatHub()
        {
            Clients.All.SendAsync("sendToAll", "Server", "Hello");
        }
    }
}