using System;
using System.Collections.Generic;
using System.IO;
using BudgetApp;
using BudgetApp.Management;

namespace BudgetApp.Database
{
    public class Database
    {
        private static Database? instance = null;
        private IFileFormat fileFormat;
        private string transactionFilePath = Path.Combine(AppContext.BaseDirectory, "Database/transactions.json");
        private string categoryFilePath = Path.Combine(AppContext.BaseDirectory, "Database/categories.json");

        private Database(IFileFormat fileFormat)
        {
            this.fileFormat = fileFormat;
        }

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database(new JsonFormat());
                    instance.Initialize();
                }
                return instance;
            }
        }

        public List<Transaction> LoadTransactions()
        {
            return fileFormat.Load(transactionFilePath);
        }

        public void SaveTransactions(List<Transaction> transactions)
        {
            fileFormat.Save(transactions, transactionFilePath);
        }

        public List<Category> LoadCategories()
        {
            return fileFormat.LoadCategories(categoryFilePath);
        }

        public void SaveCategories(List<Category> categories)
        {
            fileFormat.SaveCategories(categories, categoryFilePath);
        }
        
        public void Initialize()
        {
            LoadCategories();
            
            LoadTransactions();
            
            SaveCategories(CategoryController.GetCategories());
            
            SaveTransactions(TransactionController.GetTransactions());
        }
    }
}