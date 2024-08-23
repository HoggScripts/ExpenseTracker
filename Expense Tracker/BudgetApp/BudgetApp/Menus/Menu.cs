using System;

namespace BudgetApp;

public class Menu
{
    public string Name { get; set; }
    public bool CanBePopped { get; set; } = true;

    public Menu(string name)
    {
        Name = name;
    }

    public virtual void Run()
    {
    }
}
