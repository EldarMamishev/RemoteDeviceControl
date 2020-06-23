using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
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

        static async Task SendAsync(string message)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44397/chatter")
                .ConfigureLogging(logging => {
                    logging.AddConsole();
                }).Build();
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
            await connection.InvokeAsync("SendMessage", "Console Client", message);
        }
    }
}
