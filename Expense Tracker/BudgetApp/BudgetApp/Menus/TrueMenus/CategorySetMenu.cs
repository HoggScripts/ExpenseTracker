using System;
using System.Transactions;

namespace BudgetApp.Menus.TrueMenus
{
    public class CategorySetMenu : Menu
    {
        private SelectableMenu selectableMenu;
        private Frequency _frequency = Frequency.Daily;

        enum Frequency
        {
            Daily = 1,
            Weekly = 7,
            Monthly = 30,
            Yearly = 365,
        }
        public CategorySetMenu() : base("Category Setting Menu")
        {
            selectableMenu = new SelectableMenu("Select Category & Period for Category Budget", GetOptions());
        }

        public override void Run()
        {
            bool categorySet = false;

            while (!categorySet)
            {
                selectableMenu.Options = GetOptions();
                int selectedIndex = selectableMenu.Run();
                HandleSelection(selectedIndex);
            }
        }

        private string[] GetOptions()
        {
            var categories = CategoryController.GetCategories();
            string[] names = new string[categories.Count + 2];

            names[0] = "Frequency: " + _frequency;

            int i = 1;
            foreach (var category in categories)
            {
                names[i] = $"{i}. {category.name}";
                i++;
            }

            names[names.Length - 1] = "Add Category";

            return names;
        }

        private string DisplayBoolean(bool value) => value ? "Yes" : "No";

        private void HandleSelection(int selectedIndex)
        {
            int categoryCount = CategoryController.GetCategories().Count;

            if (selectedIndex == 0)
            {
                UpdateFrequency();
            }
            else if (selectedIndex == categoryCount + 1)
            {
                CategoryController.ReceiveNewCategory();
                MenuStack.PopMenu();
            }
            else
            {
                Category category = CategoryController.GetCategories()[selectedIndex - 1]; // Adjust index for categories
                CategoryController.SetCategoryBudget(category, (int)_frequency);
                MenuStack.PopMenu();
            }
        }



        private void UpdateFrequency()
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
                default:
                    _frequency = Frequency.Daily;
                    break;
            }
        }
    }
}

