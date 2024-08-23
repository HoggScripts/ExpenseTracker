using BudgetApp.Menus;

namespace BudgetApp
{
    public class MenuNavigator
    {
        public int Navigate(ConsoleKey keyPressed, string[] options, int selectedIndex)
        {
            if (keyPressed == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex == options.Length)
                {
                    selectedIndex = 0;
                }
            }
            else if (keyPressed == ConsoleKey.Backspace)
            {
                if (MenuStack.PeekMenu().CanBePopped)
                {
                    MenuStack.PopMenu();
                }
            }
            return selectedIndex;
        }
    }
}