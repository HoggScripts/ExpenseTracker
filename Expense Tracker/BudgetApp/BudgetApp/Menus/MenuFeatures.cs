namespace BudgetApp.Menus;

public class MenuFeatures
{
    public static ConsoleKey GetInput()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        return keyInfo.Key;
    }
}