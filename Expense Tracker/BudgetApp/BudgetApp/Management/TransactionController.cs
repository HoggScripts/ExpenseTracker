using System.Text;

namespace BudgetApp.Management
{
    using BudgetApp;
    using BudgetApp.Database;
    using BudgetApp.Menus;

    public static class TransactionController
    {
        private static List<Transaction> transactions;

        private static void LoadTransactions()
        {
            transactions = Database.Instance.LoadTransactions();
        }

        public static List<Transaction> GetTransactions()
        {
            if (transactions == null)
            {
                LoadTransactions();
            }
            return transactions;
        }

        public static void SaveTransactions()
        {
            Database.Instance.SaveTransactions(transactions);
        }
        
        public static void AddNoteToTransaction(Transaction transaction, string note)
        {
            transaction.Note = note;
            SaveTransactions();
        }

        public static string TransactionSummary(DateTime start, DateTime end)
        {
            List<Transaction> list = GetTransactionsWithinRange(start, end);
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (var transaction in list)
            {
                string transactionDisplay = $"{i}. {transaction.Display}";
                sb.AppendLine(transactionDisplay);
                i++;
            }
            if (list.Count == 0)
            {
                sb.AppendLine("No transactions for this period");
            }
            return sb.ToString();
        }

        public static List<Transaction> GetTransactionsWithinRange(DateTime start, DateTime end) => transactions.Where(c => c.Date >= start && c.Date <= end).ToList();

        public static Transaction[] GetTransactionsWithinRangeAsArray(DateTime start, DateTime end) =>
            transactions.Where(c => c.Date >= start && c.Date <= end).ToArray();


        public static void AddTransaction(Transaction newTransaction)
        {
            if (newTransaction.Category == null)
            {
                throw new ArgumentException("Transaction must have a valid category.");
            }

            transactions.Add(newTransaction);
            CategoryController.UpdateCategoryAllowance(newTransaction);
            if (BudgetController.GetBudget() != null)
            {
                BudgetController.UpdateBudgetStatus();
            }
            SaveTransactions(); 
        }

        public static void DeleteTransaction(Transaction transaction)
        {
            transaction.Category.spent -= transaction.Amount;
            transactions.Remove(transaction);
            if (BudgetController.GetBudget() != null)
            {
                BudgetController.UpdateBudgetStatus();
            }

            SaveTransactions(); 
        }
    }
}
