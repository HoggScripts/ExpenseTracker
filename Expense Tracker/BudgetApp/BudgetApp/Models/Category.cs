namespace BudgetApp
{
    using System;
    using System.Collections.Generic;

    public class Category
    {
        public string name { get; set; }
        public double? budget { get; set; }
        public double spent { get; set; }
        
        // JSON objects require a parameterless constructor
        public Category() { }

        public Category(string name)
        {
            this.name = name;
            budget = null;
        }

        public double GetDailyAllocation() => Math.Round((double)(budget/365),2);
        public double GetMonthlyAllocation() => Math.Round((double)(budget / 12), 2);
    }
}