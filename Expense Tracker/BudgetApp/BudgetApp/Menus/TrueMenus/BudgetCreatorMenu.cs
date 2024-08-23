using System;
using System.Data;

namespace BudgetApp
{
    public class BudgetCreationMenu : Menu
    {
        private Budget budget;
        private SelectableMenu selectableMenu;
        private Calendar calendar;
        private double amount { get; set; }
        private string dateNavigationInstructions = "Use arrow keys up and down to change scale. Use left and right to adjust date. Press Enter to confirm date.";

        private Frequency _frequency = Frequency.Daily;

        enum Frequency
        {
            Daily = 1,
            Weekly = 7,
            Monthly = 30,
            Yearly = 365,
        }

        private void ChangeFrequency()
        {
            switch (_frequency)
            {
                case Frequency.Daily:
                    _frequency = Frequency.Weekly;
                    break;
                case Frequency.Weekly:
                    _frequency = Frequency.Monthly;
                    break;
                case Frequency.Monthly:
                    _frequency = Frequency.Yearly;
                    break;
                case Frequency.Yearly:
                    _frequency = Frequency.Daily;
                    break;
            }
        }

        public BudgetCreationMenu() : base("Budget Creation Menu")
        {
            budget = BudgetController.GetBudget();
            selectableMenu = new SelectableMenu("Create Budget:", GetOptions());
        }

        public override void Run()
        {
            bool budgetCreated = false;

            while (!budgetCreated)
            {
                selectableMenu.Options = GetOptions();
                int selectedIndex = selectableMenu.Run();
                budgetCreated = HandleSelection(selectedIndex);
            }
        }

        private string[] GetOptions()
        {
            return new string[]
            {
                "Timeframe: " + _frequency,
                "Amount : " + amount,
                "Create Budget"
            };
        }

        private string DisplayBoolean(bool value) => value ? "Yes" : "No";

        private bool HandleSelection(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    ChangeFrequency();
                    break;
                case 1:
                    SetAmount(budget);
                    break;
                case 2:
                    CreateBudget();
                    break;
            }
            return false;
        }

        private double SetAmount(Budget budget)
        {
            Console.Clear();
            amount = Validator.ReceiveValidAmount();

            switch (_frequency)
            {
                case Frequency.Daily:
                    BudgetController.GetBudget().yearlyAllocation = amount*365;
                    return amount;
                case Frequency.Weekly:
                    BudgetController.GetBudget().yearlyAllocation = amount * 52;
                    return amount;
                case Frequency.Monthly:
                    BudgetController.GetBudget().yearlyAllocation = amount * 12;
                    return amount;
                case Frequency.Yearly:
                    BudgetController.GetBudget().yearlyAllocation = amount;
                    return amount;
            }
            return amount;
        }

        private void CreateBudget()
        {
            Console.WriteLine("Budget created! Press any key to continue");
            Console.ReadKey();
            MenuStack.PopMenu();
        }
    }
}