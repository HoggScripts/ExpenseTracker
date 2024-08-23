using BudgetApp;
using Newtonsoft.Json;
public class Transaction
{
    public string CategoryName { get; set; }
    public double Amount { get; set; }
    public DateTime Date = DateTime.Today;
    public string Note { get; set; } // Added nullable note attribute

    [JsonIgnore]
    public Category Category 
    { 
        get 
        {
            return CategoryController.GetCategory(CategoryName);
        } 
    }

    public Transaction(string categoryName, DateTime month, double amount, string note = null)
    {
        if (string.IsNullOrEmpty(categoryName))
        {
            throw new ArgumentNullException(nameof(categoryName));
        }

        CategoryName = categoryName;
        Amount = amount;
        Date = month;
        Note = note; // Assign the note
    }

    public virtual string Display
    {
        get
        {
            if (Category == null)
            {
                throw new NullReferenceException("Category object is null.");
            }
            return $"{Category.name}: £{Amount} on {Date.ToShortDateString()}";
        }
    }
}