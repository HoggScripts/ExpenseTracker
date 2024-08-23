

using BudgetApp.Management;

namespace BudgetApp.Menus.TrueMenus
{
    public class TransactionFactory
    {
        private static TransactionFactory _instance;

        public static TransactionFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TransactionFactory();
            }

            return _instance;
        }
        
        public Transaction CreateDummyTransaction(string categoryName, DateTime date, double amount) => new Transaction(categoryName, date, amount);

        public Transaction CreateTransaction(string categoryName, DateTime date, double amount, bool isIncome)
        {
            Transaction newTransaction = isIncome ? new Income(categoryName, date, amount) : new Expense(categoryName, date, amount);
            TransactionController.AddTransaction(newTransaction);
            return newTransaction;
        }

        public void CreateRecurringTransaction(string categoryName, DateTime date, double amount, bool isIncome,
            DateTime endOfRecurrence, int frequency)
            {
            TimeSpan timeSpan = endOfRecurrence - date;
            double totalDays = timeSpan.TotalDays;
            int amountOfTransactionsTotal = (int)(totalDays / frequency);
            Transaction newTransaction = CreateTransaction(categoryName, date, amount, isIncome);

            if (frequency == 1 || frequency == 7)
            {
                for (int i = 1; i <= amountOfTransactionsTotal; i++)
                {
                    DateTime nextTransactionDate = newTransaction.Date.AddDays(frequency * i);
                    CreateTransaction(categoryName, nextTransactionDate,
                        newTransaction.Amount, isIncome);
                }
            }

            if (frequency == 30)
            {
                for (int i = 1; i <= amountOfTransactionsTotal; i++)
                {
                    DateTime nextTransactionDate = newTransaction.Date.AddMonths(i);
                   CreateTransaction(categoryName, nextTransactionDate,
                        newTransaction.Amount, isIncome);
                }
            }

            if (frequency == 365)
            {
                for (int i = 1; i <= amountOfTransactionsTotal; i++)
                {
                    DateTime nextTransactionDate = newTransaction.Date.AddYears(i);
                    CreateTransaction(categoryName, nextTransactionDate,
                        newTransaction.Amount, isIncome);
                }
            }
        }
    }
}