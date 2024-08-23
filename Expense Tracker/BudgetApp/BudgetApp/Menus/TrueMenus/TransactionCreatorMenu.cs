using System;
using BudgetApp;
using BudgetApp.Menus;
using BudgetApp.Menus.TrueMenus;

using BudgetApp.Database;
using BudgetApp.Management;

public class TransactionCreationMenu : Menu
{
    private Calendar _transactionDateCalendar = new Calendar(DateTime.Today, "dd/MM/yyyy");
    private Calendar _calendarForEndOfRecurrance = new Calendar(DateTime.Today, "dd/MM/yyyy");
    private string _dummyCategoryName = "Food";
    private Transaction _dummyTransaction;
    private SelectableMenu _selectableMenu;
    private bool _isIncome, _isRecurring, _editMode;
    private Frequency _frequency = Frequency.Daily;

    enum Frequency
    {
        Daily = 1,
        Weekly = 7,
        Monthly = 30,
        Yearly = 365,
    }

    public TransactionCreationMenu() : base("Transaction Creation Menu")
    {
        TransactionFactory transactionFactory = TransactionFactory.GetInstance();
        _dummyTransaction = transactionFactory.CreateDummyTransaction(_dummyCategoryName, DateTime.Today, 0);
        _editMode = false;
        _selectableMenu = new SelectableMenu("Create Transaction:", GetOptions());
    }

    public TransactionCreationMenu(Transaction transactionToBeEdited) : base("Transaction Editor Menu")
    {
        CanBePopped = false;
        _editMode = true;
        TransactionFactory transactionFactory = TransactionFactory.GetInstance();
        _dummyTransaction = transactionFactory.CreateDummyTransaction(transactionToBeEdited.CategoryName, transactionToBeEdited.Date, transactionToBeEdited.Amount);
        _dummyCategoryName = transactionToBeEdited.CategoryName;
        _selectableMenu = new SelectableMenu("Edit Transaction:", GetOptions());
        TransactionController.DeleteTransaction(transactionToBeEdited);
    }

    private void CreateOrEditTransaction()
    {
        TransactionFactory transactionFactory = TransactionFactory.GetInstance();
        if (_isRecurring)
        {
            transactionFactory.CreateRecurringTransaction(_dummyCategoryName,
                _transactionDateCalendar.Start, _dummyTransaction.Amount, _isIncome,
                _calendarForEndOfRecurrance.Start, (int)_frequency);
        }
        else
        {
            transactionFactory.CreateTransaction(_dummyCategoryName, _transactionDateCalendar.Start,
                _dummyTransaction.Amount, _isIncome);
        }
        Console.WriteLine(_editMode ? "Transaction Edited!" : "Transaction Created!");
        Console.ReadKey();

        Database.Instance.SaveCategories(CategoryController.GetCategories());
        Database.Instance.SaveTransactions(TransactionController.GetTransactions());

        MenuStack.PopMenu();
    }

    private void UpdateAmount()
    {
        Console.Clear();
        _dummyTransaction.Amount = Validator.ReceiveValidAmount();
    }

    private void UpdateDate(Calendar calendar)
    {
        Console.WriteLine("Use arrow keys to navigate the calendar and Enter to select a date.");
        Console.WriteLine("Press Enter to confirm the selected date.");

        while (true)
        {
            Console.Clear();

            string alternateScale = GetAlternateScale(calendar._currentScale);

            Console.WriteLine("Change by " + alternateScale + "...");
            Console.WriteLine("Selected Date:");
            DisplayCalendar(calendar);

            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            calendar.NavigateCalendar(keyPressed, "yyyy/MM", "yyyy", "yyyy/MM/dd");

            if (keyPressed == ConsoleKey.Enter)
            {
                calendar.Format = "dd/MM/yyyy";
                break;
            }
        }
        _selectableMenu.Options = GetOptions();
    }

    private string GetAlternateScale(Calendar.DurationScale scale)
    {
        return scale switch
        {
            Calendar.DurationScale.Yearly => "year",
            Calendar.DurationScale.Monthly => "month",
            Calendar.DurationScale.Daily => "day",
            _ => "year"
        };
    }

    private void DisplayCalendar(Calendar calendar)
    {
        Console.WriteLine(calendar.Start.ToString(calendar.Format));
    }

    private void SetCategory()
    {
        var categories = CategoryController.GetCategories();
        var selectedIndex = new SelectableMenu("Select a category:", CategoryController.GetCategoryNamesPlusAddOption()).Run();

        if (selectedIndex == categories.Count)
        {
            _dummyCategoryName = CategoryController.ReceiveNewCategory().name;
        }
        else
        {
            _dummyCategoryName = categories[selectedIndex].name;
        }
    }

    private string DisplayRecurrenceBoolean(bool value) => value ? "Yes" : "No";

    private string DisplayIncomeBoolean(bool value) => value ? "Income" : "Expense";

    private string[] GetOptions()
    {
        var options = new List<string>
        {
            "Amount: " + _dummyTransaction.Amount,
            "Date: " + _transactionDateCalendar.Start.ToString(_transactionDateCalendar.Format),
            "Category: " + _dummyCategoryName,
            "Expense/Income: " + DisplayIncomeBoolean(_isIncome),
            "Recurring: " + DisplayRecurrenceBoolean(_isRecurring),
        };

        if (_isRecurring)
        {
            options.Add("Frequency: " + _frequency);
            options.Add("End of Recurrence: " + _calendarForEndOfRecurrance.Start.ToString(_calendarForEndOfRecurrance.Format));
        }

        options.Add(_editMode ? "Edit Transaction" : "Create Transaction");
        return options.ToArray();
    }

    private void HandleSelection(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                UpdateAmount();
                break;
            case 1:
            case 6:
                UpdateDate(selectedIndex == 1 ? _transactionDateCalendar : _calendarForEndOfRecurrance);
                break;
            case 2:
                SetCategory();
                break;
            case 3:
                _isIncome = !_isIncome;
                break;
            case 4:
                _isRecurring = !_isRecurring;
                break;
            case 5:
                if (!_isRecurring) CreateOrEditTransaction();
                else
                    UpdateFrequency();
                break;
            case 7:
                CreateOrEditTransaction();
                break;
        }
    }

    public override void Run()
    {
        while (true)
        {
            _selectableMenu.Options = GetOptions();
            int selectedIndex = _selectableMenu.Run();
            HandleSelection(selectedIndex);
        }
    }

    private void UpdateFrequency()
    {
        switch (_frequency)
        {
            case Frequency.Daily:
                _frequency = Frequency.Weekly;
                break;
            case Frequency.Weekly:
                _frequency = Frequency.Monthly;
                break;
            case Frequency.Monthly:
                _frequency = Frequency.Yearly;
                break;
            case Frequency.Yearly:
                _frequency = Frequency.Daily;
                break;
            default:
                _frequency = Frequency.Daily;
                break;
        }
    }
}
