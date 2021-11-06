using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWODA
{
    class Program
    {
        
        static void Main(string[] args)
        {
            double daily_TicketPrice = 100.00, monthly_TicketPrice = 50.00;
            Dictionary<string, int> months = new Dictionary<string, int>
            {
                { "January", 31 },
                { "Febuary", 28 },
                { "March", 31 },
                { "April", 30 },
                { "May", 31 },
                { "June", 30 },
                { "July", 31 },
                { "August", 31 },
                { "September", 30 },
                { "October", 31 },
                { "November", 30 },
                { "December", 31 }
            };

            Dictionary<string, double> ticketList = new Dictionary<string, double>();
            Dictionary<string, double> soldTickets = new Dictionary<string, double>();

            Console.WriteLine("Welcome, Agent ID: 001, What would you like to do today?");
            Task_MainMenu: Console.WriteLine("\nDo you want to sell tickets or generate report? Press S for sell, G or Report..");
            var menuKeyInfo = Console.ReadKey();
            if (menuKeyInfo.Key == ConsoleKey.S)
            {

                Task_SellTicket: Console.WriteLine("\nDo you want to sell a daily ticket or monthly ticket? Press D for daily, M for monthly");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.D)
                {
                    ticketList.Add(Guid.NewGuid().ToString(), daily_TicketPrice);
                }
                else if (keyInfo.Key == ConsoleKey.M)
                {
                    DateTime todaysDate = DateTime.Now;
                    var month = months.ElementAt(todaysDate.Month);

                    if (month.Key == "Febuary" && (todaysDate.Year % 4 == 2))
                    {
                        ticketList.Add(Guid.NewGuid().ToString(), monthly_TicketPrice * 29);
                    }
                    else
                    {
                        ticketList.Add(Guid.NewGuid().ToString(), monthly_TicketPrice * month.Value);
                    }
                }
                else if (keyInfo.Key != ConsoleKey.D && keyInfo.Key != ConsoleKey.M)
                {
                    goto Task_SellTicket;
                }

                Console.WriteLine("\nYour Ticket Details are:");
                for (int i = 0; i < ticketList.Keys.Count; i++)
                {
                    Console.WriteLine("{0}. Ticket ID: {1}, Price: N{2}", i + 1, ticketList.Keys.ElementAt(i), ticketList.Values.ElementAt(i).ToString("0.00"));
                }

                Task_ProceedWithPayment: Console.WriteLine("\nDo you want to proceed with this request? Press Y for yes and N for no..");
                ConsoleKeyInfo proceedKeyInfo = Console.ReadKey();
                if (proceedKeyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("\n");
                    foreach (var item in ticketList)
                    {
                        soldTickets.Add(item.Key, item.Value);
                        ticketList.Clear();
                        Console.WriteLine("Printing Tickets...");
                        Console.WriteLine("\nTransaction Completed...");
                        goto Task_MainMenu;
                    }
                }
                else if (proceedKeyInfo.Key == ConsoleKey.N)
                {
                    ticketList.Clear();
                    Console.WriteLine("\n");
                    goto Task_MainMenu;
                }
                else
                {
                    Console.WriteLine("\nWrong Input!!!");
                    goto Task_ProceedWithPayment;
                }
            }
            else if (menuKeyInfo.Key == ConsoleKey.G)
            {
                Console.WriteLine("\nYour Sold Ticket Details are:");
                double sum = 0; 
                for (int i = 0; i < soldTickets.Keys.Count; i++)
                {
                    sum += soldTickets.Values.ElementAt(i);
                    Console.WriteLine("{0}. Ticket ID: {1}, Price: N{2}", i + 1, soldTickets.Keys.ElementAt(i), soldTickets.Values.ElementAt(i).ToString("0.00"));
                }

                double Chairman = (sum * (0.65)), you = (sum * (0.35));
                Console.WriteLine("Chairman's Payment is: N{0}",Chairman.ToString("0.00") );
                Console.WriteLine("Your Income is: N{0}", you.ToString("0.00"));
                Task_ProcessPayment: Console.WriteLine("\nPress Y to process payment or N to go back to the main menu");
                ConsoleKeyInfo processPaymentKeyInfo = Console.ReadKey();
                if (processPaymentKeyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("\nProcessing Payment \nPayment has been processed...");
                    Console.WriteLine("\nPress any key to close the app...");
                    Console.ReadKey();
                }
                else if (processPaymentKeyInfo.Key == ConsoleKey.N)
                {
                    goto Task_MainMenu;
                }
                else
                {
                    Console.WriteLine("\nWrong Input!!!");
                    goto Task_ProcessPayment;
                }
                
            }
            else
            {
                Console.WriteLine("Wrong Input!!!");
                goto Task_MainMenu;
            }
        }
    }
}
