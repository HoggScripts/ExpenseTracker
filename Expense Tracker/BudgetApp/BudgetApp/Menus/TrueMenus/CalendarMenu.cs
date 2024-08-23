using System.Text;
using BudgetApp.Management;
using BudgetApp.Menus;
using Spectre.Console;

namespace BudgetApp
{
    public class CalendarMenu : Menu
    {
        private readonly Calendar calendar = new Calendar();

        public CalendarMenu() : base("Calendar")
        {
        }

        public override void Run()
        {
            while (true)
            {
                DisplayMenu();
                NavigateMenu();
            }
        }

        private void NavigateMenu()
        {
            var keyPressed = MenuFeatures.GetInput();
            if (keyPressed == ConsoleKey.Backspace)
            {
                MenuStack.PopMenu();
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                var transactions = TransactionController.GetTransactionsWithinRangeAsArray(calendar.Start, calendar.End);
                if (transactions.Length > 0)
                {
                    MenuStack.PushMenu(new TransactionSelectionMenu("Transaction Selection Menu", transactions));
                }
            }
            else
            {
                calendar.NavigateCalendar(keyPressed, "yyyy/MM", "yyyy", "yyyy/MM/dd");
            }
        }
        
        private void DisplayMenu()
        {
            Console.Clear();
            var asciiArt = new FigletText("Budget App");
            AnsiConsole.Render(asciiArt.Color(Color.Green));

            var table1 = new Table() { Border = TableBorder.Rounded };
            table1.AddColumn(new TableColumn("[yellow]" + calendar._currentScale + " " + calendar.Start.ToString(calendar.Format) + "[/]").Centered());
            table1.Width(75);
            AnsiConsole.Render(table1);

            var table2 = new Table() { Border = TableBorder.Rounded };
            table2.AddColumn(new TableColumn("Transaction Summary").LeftAligned());
            table2.AddColumn(new TableColumn(RenderBudgetBreakdown()).LeftAligned());
            table2.AddRow(new Markup(RenderTransactionSummary()).LeftJustified(), new Markup(RenderCategorySummary()).Centered());
            table2.Width(75);
            AnsiConsole.Render(table2);
        }


        private string RenderTransactionSummary()
        {
            return TransactionController.TransactionSummary(calendar.Start, calendar.End);
        }

        private string RenderBudgetBreakdown()
        {
            if (Budget.Instance.yearlyAllocation == 0)
            {
                return "No overall budget set.";
            }
            else
            {
                return BudgetController.BudgetSummary(calendar.Start, calendar.End, calendar);
            }
        }

        private string RenderCategorySummary()
        {
            var sb = new StringBuilder();
            foreach (var summary in CategoryController.CategorySummary(calendar.Start, calendar.End, calendar))
            {
                sb.AppendLine(summary);
            }
            return sb.Length == 0 ? "No categories to display" : sb.ToString();
        }
    }
}