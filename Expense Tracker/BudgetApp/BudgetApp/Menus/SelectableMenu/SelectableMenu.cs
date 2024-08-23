using System;
using System.Data;
using BudgetApp.Menus;
using Spectre.Console;


namespace BudgetApp
{
    public class SelectableMenu
    {
        private MenuNavigator _menuNavigator;
        public MenuDisplay _menuDisplay;
        public string[] Options { get; set; }
        private string Prompt { get; }
        private int selectedIndex;

        public SelectableMenu(string prompt, string[] options)
        {
            this._menuNavigator = new MenuNavigator();
            this._menuDisplay = new MenuDisplay();
            this.Options = options;
            this.Prompt = prompt;
            this.selectedIndex = 0;
        }

        public int Run()
        {
            while (true)
            {
                _menuDisplay.DisplayOptions(Prompt, Options, selectedIndex);
                var keyPressed = MenuFeatures.GetInput();
                selectedIndex = _menuNavigator.Navigate(keyPressed, Options, selectedIndex);
                if (keyPressed == ConsoleKey.Enter)
                {
                    return selectedIndex;
                }
            }
        }
    }
}

        