using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab_4
{
    internal class Program
    {
        static InternetProvider GetInternetProvider()
        {
            Console.WriteLine("Enter the name of the provider: ");
            string name = Console.ReadLine() ?? "";
            if (name == string.Empty)
            {
                throw new Exception("Invalid name");
            }

            Console.WriteLine("Enter the phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Enter the address: ");
            string address = Console.ReadLine() ?? "";
            if (name == string.Empty)
            {
                throw new Exception("Invalid address");
            }

            Console.WriteLine("Enter the connection cost: ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal connectionCost))
            {
                throw new Exception("Invalid input for connection cost");
            }

            return new InternetProvider(name, phoneNumber, address, connectionCost);
        }

        static void PrintTable(InternetProvider[] sortedList)
        {
            Console.WriteLine(new string('=', 88));
            foreach (var item in sortedList)
            {
                Console.WriteLine($"| {item.Name,-20} | {item.Address,-30} | {item.PhoneNumber,-15} | {item.ConnectionCost,-10} |");
            }
            Console.WriteLine(new string('=', 88));

        }
        static void Main(string[] args)
        {
            TextReader fileReader = File.OpenText("D:/VB .Net/lab4/Lab 4/internetProviders.csv");
            var csv = new CsvReader(fileReader, CultureInfo.InvariantCulture);

            InternetProvider[] data = csv.GetRecords<InternetProvider>().ToArray();

            PrintTable(data.ToArray());

            string userInput;
            do
            {
                Console.WriteLine("Enter new provider? [Y/N]");
                userInput = Console.ReadLine() ?? "n";
                if (userInput.ToLower() == "y")
                {
                    try
                    {
                        var provider = GetInternetProvider();
                        data = data.Append(provider).ToArray();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Try again\n");
                    }
                }
            } while (userInput.ToLower() == "y");

            PrintTable(data);

            var filteredList = data.Where(x => x.Name.ToLower().StartsWith('s'));

            var file = new FileInfo("D:/VB .Net/lab4/Lab 4/doc.txt");
            if (!file.Exists)
            {
                file.Create().Close();
            }
            var writer = file.CreateText();
            foreach (var provider in filteredList)
            {
                writer.WriteLine($"| {provider.Name,-20} | {provider.Address,-30} | {provider.PhoneNumber,-15} | {provider.ConnectionCost,-10} |");
            }
            writer.Close();

            Console.WriteLine("\nFiltered list:\n");
            PrintTable(filteredList.ToArray());

            var averageCost = data.Average(x => x.ConnectionCost);
            var sortedList = data.OrderBy(x => x.PhoneNumber);

            Console.WriteLine("\nSorted list:\n");
            PrintTable(sortedList.ToArray());
            Console.WriteLine("\nAverage cost: {0:0.000}", averageCost);

        }
    }
}