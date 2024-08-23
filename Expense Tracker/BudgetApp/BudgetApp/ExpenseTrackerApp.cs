using System;
using System.Collections.Generic;
using BudgetApp;
using BudgetApp.Database;
using BudgetApp.Menus.TrueMenus;

    public static class ExpenseTrackerApp
    {
        public static void Run()
        {
            // Initialize the Database class and load the categories from the database
            Database.Instance.Initialize();
            Budget.Initialize(0);

            // Add default categories
            var defaultCategories = new List<string> { "Food", "Bills", "Entertainment", "Clothes", "Transportation" };
            foreach (var categoryName in defaultCategories)
            {
                if (CategoryController.GetCategory(categoryName) == null)
                {
                    var newCategory = new Category(categoryName);
                    CategoryController.GetCategories().Add(newCategory);
                }
            }

            // Save the categories to the database
            Database.Instance.SaveCategories(CategoryController.GetCategories());

            MenuStack.PushMenu(new MainMenu());
        }
    }
