using System; 
using BudgetApp.Database;
using BudgetApp;
using System.Transactions;

using BudgetApp.Management;
public static class CategoryController
{
    private static List<Category> categories;

    public static List<Category> Categories
    {
        get
        {
            if (categories == null)
            {
                categories = Database.Instance.LoadCategories();
            }
            return categories;
        }
    }
    public static Category GetCategory(string categoryName)
    {
        if (Categories == null)
        {
            throw new Exception("Categories is null. Make sure to load the categories before accessing them.");
        }

        return Categories.Find(c => c.name == categoryName);
    }

    public static List<Category> GetCategories() => Categories;

    public static string[] GetCategoryNamesPlusAddOption()
    {
        int n = 1;
        string[] categoryNames = new string[categories.Count + 1];
        for (int i = 0; i < categories.Count; i++)
        {
            categoryNames[i] = $"{n}. {categories[i].name}";
            n++;
        }
        categoryNames[categoryNames.Length - 1] = "Add Category";
        return categoryNames;
    }

    public static void UpdateCategoryAllowance(Transaction newTransaction) => newTransaction.Category.spent += newTransaction.Amount;
    
    public static Category ReceiveNewCategory()
    {
        while (true)
        {
            Console.WriteLine("Enter a new category name:");
            string categoryName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoryName) && !categories.Any(c => c.name.ToUpper() == categoryName.ToUpper()))
            {
                Console.WriteLine($"New Category {categoryName} created!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                categories.Add(new Category(categoryName));

                Database.Instance.SaveCategories(categories); 

                return GetCategory(categoryName);
            }
            else
            {
                Console.WriteLine("Please type a valid category, that does not already exist.");
            }
        }
    }

    public static void SetCategoryBudget(Category category, int frequency)
    {
        Console.WriteLine($"How much would you like to allocate to {category.name}");
        double budget = Validator.ReceiveValidAmount();
        switch (frequency)
        {
            case 1:
                category.budget = budget*365;
                break;
            case 7:
                category.budget = budget * 52; ;
                break;
            case 30:
                category.budget = budget* 12;
                break;
            case 365:
                category.budget = budget;
                break;
        }
        Console.WriteLine($"Budget of {budget} set for {category.name}");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        //Update with existing transactions once set
        foreach (var transaction in TransactionController.GetTransactions())
        {
            if (transaction.Category == category)
            {
                category.spent += transaction.Amount;
            }
        }
    }


    public static List<string> CategorySummary(DateTime start, DateTime end, Calendar calendar)
    {
        Dictionary<string, double> summaryPerCategory = new Dictionary<string, double>();

        //Each category summarised in a dictionary (name (key) : spending (value))
        foreach (var category in CategoryController.categories)
        {
            summaryPerCategory[category.name] = 0;
        }

        //Get spending per period selected from Menu
        foreach (var transaction in TransactionController.GetTransactions())
        {
            if (Validator.TransactionInPeriod(transaction.Date, start, end) && transaction.GetType() == typeof(Expense))
            {
                summaryPerCategory[transaction.Category.name] += transaction.Amount;
            }
        }

        List<string> categorySummaries = new List<string>();

        foreach (var KvP in summaryPerCategory)
        {
            if (CategoryController.categories.Any(c => c.budget != null && c.name == KvP.Key))
            {
                if (KvP.Value > 0)
                {
                    double roundedValue = Math.Round(KvP.Value, 2);
                    
                    switch (calendar._currentScale)
                    {
                        case Calendar.DurationScale.Yearly:
                            categorySummaries.Add($"Total spent on {KvP.Key}: £{roundedValue} / £{(float)CategoryController.GetCategory(KvP.Key).budget} (yearly)");
                            break;
                        case Calendar.DurationScale.Monthly:
                            categorySummaries.Add($"Total spent on {KvP.Key}: £{roundedValue} /  £{(float)CategoryController.GetCategory(KvP.Key).GetMonthlyAllocation()} (monthly)");
                            break;
                        case Calendar.DurationScale.Daily:
                            categorySummaries.Add($"Total spent on {KvP.Key}: £{roundedValue} /  £{(float)CategoryController.GetCategory(KvP.Key).GetDailyAllocation()} (daily)");
                            break;
                    }
                }
                   
            }
            else
            {
                if (KvP.Value > 0)
                {
                    double roundedValue = Math.Round(KvP.Value, 2);
                    categorySummaries.Add($"Total spent on {KvP.Key}: £{roundedValue}");
                }
            }
        }
        return categorySummaries;
    }
}


