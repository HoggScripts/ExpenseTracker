namespace BudgetApp;

public class Income : Transaction
{
	public Income(string categoryName, DateTime month, double amount) : base(categoryName, month, amount)
	{
	}

	public override string Display => $"[green]{Category.name}: + £{Amount} on {Date.ToShortDateString()}[/]";
}

