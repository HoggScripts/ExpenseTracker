using System.Diagnostics;
using BudgetApp.Management;
using static BudgetApp.Calendar;

namespace BudgetApp;

public static class BudgetController
{
    public static Budget GetBudget() => Budget.Instance;

    public static void UpdateBudgetStatus()
    {
        Budget.Instance.amountSpent = 0;
        foreach (var transaction in TransactionController.GetTransactions())
        {
            if (transaction.GetType() == typeof(Expense))
            {
                Budget.Instance.amountSpent += transaction.Amount;
            }
        }
    }

    public static string BudgetSummary(DateTime start, DateTime end, Calendar calendar)
    {
        double amountSpent = 0;
        int n = 1;
        foreach (var transaction in TransactionController.GetTransactions())
        {
            if (Validator.TransactionInPeriod(transaction.Date, start, end) & transaction.GetType() == typeof(Expense))
            {
                amountSpent += transaction.Amount;
            }
        }
        switch (calendar._currentScale)
        {
            case Calendar.DurationScale.Yearly:
                return $"Total spent {amountSpent} / {Budget.Instance.yearlyAllocation}";
            case Calendar.DurationScale.Monthly:
                return $"Total spent {amountSpent} / {Budget.Instance.GetMonthlyAllocation()}";  
            case Calendar.DurationScale.Daily:
                return $"Total spent {amountSpent} / {Budget.Instance.GetDailyAllocation()}";
        }
        return "No Overall Budget Set";
    }
}
