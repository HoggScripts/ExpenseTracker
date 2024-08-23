namespace BudgetApp;

public class BudgetSetMenu
{
    SelectableMenu _menu = new SelectableMenu("Please select an option:", new string[] { "Set Budget", "Clear Budget", "Back" });
    
    public void RunMenu()
    {
        int selection = _menu.NavigateMenu();
        switch (selection)
        {
            case 0:
                Console.WriteLine("Set Budget...");
                break;
            case 1:
                Console.WriteLine("Clear Budget...");
                break;
            case 2:
                Console.WriteLine("Back...");
                break;
        }
    }
    
}