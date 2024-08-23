using System;

namespace BudgetApp
{
    public static class MenuStack
    {
        public static Stack<Menu> menuStack = new Stack<Menu>();

        public static void PushMenu(Menu menu)
        {
            Console.WriteLine("MENU COUNT " + menuStack.Count);
            menuStack.Push(menu);
            menu.Run();
        }

        public static void PopMenu()
        {
            if (menuStack.Count > 0)
            {
                menuStack.Pop();
            }

            Menu menu = PeekMenu();
            if (menuStack.Count > 0)
            {
                menu.Run();
            }  
        }

        internal static Menu PeekMenu()
        {
            try
            {
                return menuStack.Peek();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("You are already at the main menu" + ex.Message);
                return null;
            }
        }
    }
}