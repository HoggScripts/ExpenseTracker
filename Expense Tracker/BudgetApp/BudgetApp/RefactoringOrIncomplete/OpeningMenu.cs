using BudgetApp;

namespace BudgetApp;


public class OpeningMenu
{
    SelectableMenu _menu = new SelectableMenu("Welcome to BudgetApp! Please select an option:", new string[] { "start", "about", "Exit" });

    public void RunMenu()
    {
        int selection = _menu.NavigateMenu();
        switch (selection)
        {
            case 0:
                DurationSelector menu3 = new DurationSelector();
                menu3.DisplayMenu();
                
                break;
            case 1:
                Console.WriteLine("About BudgetApp...");
                break;
            case 2:
                Console.WriteLine("Exiting BudgetApp...");
                break;
        }
    }
}