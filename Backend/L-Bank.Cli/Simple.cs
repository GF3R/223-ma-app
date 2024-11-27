using L_Bank_W_Backend.Core.Models;
using L_Bank_W_Backend.DbAccess.Repositories;

namespace L_Bank.Cli;

public static class Simple
{
    public static void Run(ILedgerRepository ledgerRepository)
    {
        ////////////////////
        // Your Code Here
        ////////////////////
        /// 
        Random random = new();
        var allLedgers = ledgerRepository.GetAllLedgers().ToArray();
        Console.WriteLine();
        Console.WriteLine("Press Esc to Stop");
        do
        {
            while (!Console.KeyAvailable)
            {
                var from = allLedgers[random.Next(allLedgers.Length)];
                var to = allLedgers[random.Next(allLedgers.Length)];
                var amount = random.NextInt64(1, 101);
                ledgerRepository.Book(amount, from, to);
                Console.Write(".");
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        
        Console.WriteLine();
        Console.WriteLine("Getting total money in system at the end.");
        try
        {
            decimal startMoney = ledgerRepository.GetTotalMoney();
            Console.WriteLine($"Total end money: {startMoney}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in getting total money.");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Hello, World!");
    }
}