namespace BudgetApp.Database;

public interface IFileFormat
{
    public abstract List<Transaction> Load(string filePath);
    public abstract void Save(List<Transaction> transactions, string filePath);
    public abstract List<Category> LoadCategories(string filePath);
    public abstract void SaveCategories(List<Category> categories, string filePath);
}