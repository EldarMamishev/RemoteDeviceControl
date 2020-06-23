using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IOTEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            SendAsync(input).Wait();
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            try
            {

                var hubConnection = new HubConnection("http://localhost:44397");
                //hubConnection.TraceLevel = TraceLevels.All;
                //hubConnection.TraceWriter = Console.Out;
                IHubProxy hubProxy = hubConnection.CreateHubProxy("ChatHub");
                hubProxy.On<string, string>("sendToAll", (name, message) =>
                {
                    Console.WriteLine("Incoming data: {0} {1}", name, message);
                });
                ServicePointManager.DefaultConnectionLimit = 10;
                await hubConnection.Start();

            }
            catch (Exception ex)
            {

            }
        }

        static async Task SendAsync(string message)
        {
            try
            {

                var hubConnection = new HubConnection("http://localhost:44397");
                hubConnection.TraceLevel = TraceLevels.All;
                hubConnection.TraceWriter = Console.Out;
                await hubConnection.Start();
                IHubProxy hubProxy = hubConnection.CreateHubProxy("ChatHub");
                await hubProxy.Invoke("sendToAll", message);

                hubProxy.On<string, string>("sendToAll", (name, message) =>
                {
                    Console.WriteLine("Incoming data: {0} {1}", name, message);
                });

                ServicePointManager.DefaultConnectionLimit = 10;
                await hubConnection.Start();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
