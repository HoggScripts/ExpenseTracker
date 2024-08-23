
namespace BudgetApp
{
    public class Budget
    {
        public double yearlyAllocation { get; set; }
        public double amountSpent { get; set; }

        
        private Budget(double totalAllocatedAmount)
        {
            this.yearlyAllocation = totalAllocatedAmount;
        }

        private static Budget _instance;

        public static Budget Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Budget instance has not been initialized.");
                }
                return _instance;
            }
        }

        public static void Initialize(double totalAllocatedAmount)
        {
            if (_instance != null)
            {
                throw new Exception("Budget instance has already been initialized.");
            }
            _instance = new Budget(totalAllocatedAmount);
        }

        public double GetDailyAllocation() => Math.Round((yearlyAllocation / 365), 2);
        public double GetMonthlyAllocation() => Math.Round((yearlyAllocation / 12), 2);
    }
}