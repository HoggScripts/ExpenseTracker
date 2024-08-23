using System;

namespace BudgetApp
{
    public class MainMenu
    {
        DurationSelector menu3 = new DurationSelector();
        public void RunMainMenu()
        {
            //SelectableMenu mainMenu = new SelectableMenu("Welcome to the Budget App. Please select an option:", new []{"Start", "About", "Exit"});
            //int userChoice = mainMenu.Run();

            while (true)
            {
                DisplayOptions();
                Console.WriteLine("Please select an option");
                Console.WriteLine();
                int userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:

                        //BenDHogg to implement where dates come from
                        //TransactionController.GetAllTransactions();
                        break;
                    case 2:

                        Console.WriteLine("Budget for october set at 1000.");
                        BudgetController.CreateOverallBudget("October",new DateTime(2024,10,01),new DateTime(2024,10,20), 1000);
                        break;
                    case 3:
                        //menu3.DisplayMenu();
                        // BudgetController.BudgetBreakdown(BudgetController.GetBudget("October"));
                        //BudgetController.CategorySummaryForBudget(BudgetController.GetBudget("October"));
                        BudgetController.CalculateOverallSpending(BudgetController.GetBudget("October"));

                        break;

                    case 4:
                        Console.WriteLine("Please enter the category name");
                        string categoryName = Console.ReadLine();
                        CategoryController.CreateCategory(categoryName);

                        Console.WriteLine("Would you like to set a budget for this category?");
                        string response = Console.ReadLine();

                        if (response.ToUpper() == "Y")
                        {
                            CategoryController.SetCategoryBudget(CategoryController.GetCategory(categoryName));
                        }
                        else
                        {
                            Console.WriteLine("Thanks, category added with no budget.");
                        }

                            break;
                    case 5:
                        Console.WriteLine("Please select a category");
                        CategoryController.SetCategoryBudget(CategoryController.GetCategory("Food"));
                       
                        break;
                    case 6:

                        CategoryController.SetCategoryBudget(CategoryController.GetCategory("Food"));
                        CategoryController.SetCategoryBudget(CategoryController.GetCategory("Bills"));
                        //CategoryController.SetCategoryBudget(CategoryController.GetCategory("Entertainment"));
                        //CategoryController.SetCategoryBudget(CategoryController.GetCategory("Clothes"));

                        Transaction food1 = new Transaction(CategoryController.GetCategory("Food"), new DateTime(2024, 10, 02), 100, true, true);
                        Transaction bills1 = new Transaction(CategoryController.GetCategory("Bills"), new DateTime(2024, 10, 02), 100, true, true);
                        Transaction grub1 = new Transaction(CategoryController.GetCategory("Entertainment"), new DateTime(2024, 10, 02), 100, true, true);
                        Transaction clothes1 = new Transaction(CategoryController.GetCategory("Clothes"), new DateTime(2024, 10, 03), 100, true, true);

                        TransactionController.AddTransactions(food1);
                        TransactionController.AddTransactions(bills1);
                        TransactionController.AddTransactions(grub1);
                        TransactionController.AddTransactions(clothes1);

                        BudgetController.UpdateBudgetStatus(food1);
                        BudgetController.UpdateBudgetStatus(bills1);
                        BudgetController.UpdateBudgetStatus(grub1);
                        BudgetController.UpdateBudgetStatus(clothes1);



                        break;
                    case 7:

                        Console.WriteLine("Categories List ");
                        Console.WriteLine("-------------");
                        int n = 1;
                        foreach (var category in CategoryController.categories)
                        {
                           
                            Console.WriteLine($"{n}. {category.name}");
                            n++;
                        }
                        Console.WriteLine("------------");
                        Console.WriteLine();
                        break;
                      
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            
        }

        public static void DisplayOptions()
        {
            Console.WriteLine("1. See All Transactions");
            Console.WriteLine("2. Set Budget");
            Console.WriteLine("3. View Budget");
            Console.WriteLine("4. Add Category");
            Console.WriteLine("5. Set Budget for Category");
            Console.WriteLine("6. Add Transaction");
            Console.WriteLine("7. See all categories");
        }
    }
}