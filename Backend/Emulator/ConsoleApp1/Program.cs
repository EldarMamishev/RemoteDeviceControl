using Data;
using Data.DataAccess;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;

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
            var httpClient = new HttpClient();

            ConsoleDbContext appDbContext = new ConsoleDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            var log = unitOfWork.LogEntityRepository.Get().ToList().LastOrDefault();
            //if (log.ActionTime.AddMinutes(2) < DateTime.Now)
            //{
            //    Console.WriteLine("No connections");
            //    return;
            //}

            var device = unitOfWork.DeviceRepository.GetById(log.DeviceId.Value);
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
                            Console.WriteLine($"Lift is called to floor {num}.");
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
                            log.Comments += $"Door {device.Name} is locked.";
                            break;
                        } 
                        else if (k.KeyChar == '2')
                        {
                            Console.WriteLine("Door is unlocked");
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

            unitOfWork.LogEntityRepository.Update(log);
            unitOfWork.Save();
        }
    }
}
