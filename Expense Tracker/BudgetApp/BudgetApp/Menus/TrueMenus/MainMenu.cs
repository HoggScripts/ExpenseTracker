using System;
using BudgetApp.Menus.TrueMenus;
using Spectre.Console;

namespace BudgetApp
{
    public class MainMenu : Menu
    {
        private SelectableMenu selectableMenu = new SelectableMenu("Main Menu", new string[] {"View Transactions", "Add Income/Expense", "Add Budget", "Manage Categories", "Exit"});
        
        public MainMenu() : base("Main Menu")
        {
            CanBePopped = false;
        }
        
        public override void Run()
        {
            int selection = selectableMenu.Run();
            switch (selection)
            {
                case 0:
                    MenuStack.PushMenu(new CalendarMenu());
                    break;
                case 1:
                    MenuStack.PushMenu(new TransactionCreationMenu());
                    break;
                case 2:
                    MenuStack.PushMenu(new BudgetCreationMenu());
                    break;
                case 3:
                    MenuStack.PushMenu(new CategorySetMenu());
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}