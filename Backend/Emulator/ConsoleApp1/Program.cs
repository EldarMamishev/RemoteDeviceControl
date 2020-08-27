using Core.Entities;
using Core.Enums;
using Data;
using Data.DataAccess;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using ViewModel.Connection;

namespace ConsoleApp1
{
    public class ConsoleDbContext : AppDbContext
    {
        public ConsoleDbContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:remotecontraoldbserver.database.windows.net,1433;Initial Catalog=RemoteControl_db;Persist Security Info=False;User ID=Eldar;Password={Admin123!};Encrypt=True;TrustServerCertificate=False;");
    }

    class Program
    {
        static void Main(string[] args)
        {
            string url = @"https://eldarremotecontrol.azurewebsites.net/api/Device/";
            var restClient = new BaseClient(url);
            int personId = 0;
            ConnectionResponse trueConnection;

            while (true)
            {
                Console.Write("Write your id: ");
                var id = Console.ReadLine();
                if (!Int32.TryParse(id, out personId))
                {
                    Console.WriteLine("Id should be a positive integer.");
                    continue;
                }

                var restResult = restClient.Get<int>("CheckPerson", parameters: new Dictionary<string, string>() { { nameof(personId), id } });
                if (restResult == null)
                {
                    Console.WriteLine("Person does not exist.");
                    continue;
                }

                break;
            }

            var parameters = new Dictionary<string, string>()
            { 
                { 
                    nameof(personId), personId.ToString() 
                } 
            };

            var connections = restClient.Get<List<ConnectionResponse>>("GetActiveConnections", parameters: parameters);
            if (connections == null || connections.Data.Count == 0)
            {
                Console.WriteLine("No active connections");
                return;
            }

            while (true)
            {
                int i = 1;
                foreach(var c in connections.Data)
                {
                    Console.WriteLine($"{i}: Device id={c.DeviceId}");
                    i++;
                }

                Console.Write("Choose number of device: ");
                var k = Console.ReadLine();
                int deviceNumber = 0;
                if (!Int32.TryParse(k, out deviceNumber) || deviceNumber >= i || deviceNumber < 0)
                    continue;

                trueConnection = connections.Data[deviceNumber - 1];
                break;
            }

            var body = new ConnectionViewModel()
            {
                deviceId = trueConnection.DeviceId,
                personId = trueConnection.PersonId
            };


            var logResponse = restClient.Post<int>("ConnectFromDevice", JsonConvert.SerializeObject(body));

            ConsoleDbContext appDbContext = new ConsoleDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            var connection = unitOfWork.ConnectionRepository.GetById(trueConnection.ConnectionId);
            var device = unitOfWork.DeviceRepository.GetById(trueConnection.DeviceId);
            device.Status = DeviceStatus.Active;
            unitOfWork.DeviceRepository.Update(device);
            LogEntity log = new LogEntity()
            {
                DeviceId = trueConnection.DeviceId,
                ActionTime = DateTime.Now                
            };
            string currentState = string.Empty;

            while (true)
            {
                string options;
                if (device.Type == Core.Enums.DeviceType.Lift)
                {
                    options = "Press 1 to call Lift;";
                    Console.WriteLine(options);
                    var k = Console.ReadKey();
                    Console.WriteLine();
                    if (k.KeyChar != '1')
                    {
                        Console.WriteLine("Try again");
                        continue;
                    }
                    while (true)
                    {
                        Console.WriteLine("Which floor?");
                        var floor = Console.ReadLine();
                        if (int.TryParse(floor, out int num))
                        {
                            log.Comments += $"Lift {device.Name} is called to floor {num}.";
                            currentState = $"Lift {device.Name} is on {num} floor.";
                            Console.WriteLine($"Lift  is called to floor {num}.");
                            break;
                        }
                    }
                    break;
                }
                else if (device.Type == Core.Enums.DeviceType.Lock)
                {
                    if (device.Type == Core.Enums.DeviceType.Lift)
                    {
                        options = "Press 1 to lock the door;\nPress 2 to unclock the door";
                        Console.WriteLine(options);
                        var k = Console.ReadKey();
                        if (k.KeyChar != '1' && k.KeyChar != '2')
                        {
                            Console.WriteLine("Try again");
                            continue;
                        }
                        else if (k.KeyChar == '1')
                        {
                            Console.WriteLine("Door is locked");
                            currentState = $"Door {device.Name} is locked.";
                            log.Comments += $"Door {device.Name} is locked.";
                            break;
                        }
                        else if (k.KeyChar == '2')
                        {
                            Console.WriteLine("Door is unlocked");
                            currentState = $"Door {device.Name} is unlocked.";
                            log.Comments += $"Door {device.Name} is unlocked.";
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    options = "No options for such device.";
                    log.Comments += $"Access denied.";
                    Console.WriteLine(options);
                    break;
                }
            }

            device.ActiveState = currentState;
            device.Status = DeviceStatus.Sleeping;
            connection.FinishDateUTC = DateTime.Now;
            unitOfWork.LogEntityRepository.Update(log);
            unitOfWork.ConnectionRepository.Update(connection);
            unitOfWork.DeviceRepository.Update(device);
            unitOfWork.Save();
        }
    }
}
