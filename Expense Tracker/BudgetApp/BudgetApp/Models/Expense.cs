namespace BudgetApp;

public class Expense : Transaction
{
	public Expense(string categoryName, DateTime month, double amount) : base(categoryName, month, amount)
	{
	}

	public override string Display => $"[red]{Category.name}: - £{Amount} on {Date.ToShortDateString()}[/]";
}

