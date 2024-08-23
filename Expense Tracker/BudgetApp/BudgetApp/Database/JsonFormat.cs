using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BudgetApp.Database
{
    public class JsonFormat : IFileFormat
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public List<Transaction> Load(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonContent, settings);
            return transactions;
        }

        public void Save(List<Transaction> transactions, string filePath)
        {
            string jsonContent = JsonConvert.SerializeObject(transactions, settings);
            File.WriteAllText(filePath, jsonContent);
        }

        public List<Category> LoadCategories(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(jsonContent, settings);
            return categories;
        }

        public void SaveCategories(List<Category> categories, string filePath)
        {
            string jsonContent = JsonConvert.SerializeObject(categories, settings);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}