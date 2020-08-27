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
                .WithUrl("https://eldarremotecontrol.azurewebsites.net/chatter")
                .ConfigureLogging(logging => {
                    logging.AddConsole();
                }).Build();
            await Task.Delay(new Random().Next(0, 5) * 1000);
            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            await connection.InvokeAsync("SendMessage", "Console Client", message);
        }

        public Task ReceiveMessage(string user, string message)
        {
            Console.WriteLine(user + message);

            return Task.CompletedTask;
        }
    }
}