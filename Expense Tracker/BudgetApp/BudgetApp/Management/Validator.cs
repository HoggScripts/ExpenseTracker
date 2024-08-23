using System;
namespace BudgetApp;
public static class Validator
{
    public static bool TransactionInPeriod(DateTime transactionDate, DateTime start, DateTime end) => transactionDate >= start && transactionDate <= end;

    public static double ReceiveValidAmount()
    {
        while (true)
        {
            Console.Write("Enter amount: ");
            var amount = Console.ReadLine();
            if (double.TryParse(amount, out double result) && double.IsPositive(result))
            {
                return Math.Round(result, 2);
            }
            Console.WriteLine("Please enter a positive number");
        }
    }
}


