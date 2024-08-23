using BudgetApp.Management;
using BudgetApp.Menus.TrueMenus;


namespace BudgetApp;

public class TransactionSelectionMenu : Menu
{
    private SelectableMenu _selectableMenu;
    private Transaction[] transactions;

    public TransactionSelectionMenu(string name, Transaction[] transactions) : base(name)
    {
        this.transactions = transactions;
    }

    private string[] GetOptions()
    {
        string[] options = new string[transactions.Length];
        for (int i = 0; i < transactions.Length; i++)
        {
            options[i] = transactions[i].Display;
        }
        return options;
    }

    private void FillTransactionMenu()
    {
        _selectableMenu = new SelectableMenu("Select Transaction to Edit:", GetOptions());
    }
    
    private void HandleSelection(int selectedIndex)
    {
        Transaction selectedTransaction = transactions[selectedIndex];
        Console.WriteLine("You have selected: " + selectedTransaction.Display);
        SelectableMenu decisionMenu = new SelectableMenu("What would you like to do?", new string[] {"Edit", "Delete", "View Note", "Back"});
        int decision = decisionMenu.Run();
        switch (decision)
        {
            case 0:
                MenuStack.PushMenu(new TransactionCreationMenu(selectedTransaction));
                break;
            case 1:
                TransactionController.DeleteTransaction(selectedTransaction);
                Console.WriteLine("Transaction Deleted!");
                Console.ReadKey();
                MenuStack.PopMenu();
                break;
            case 2:
                if (selectedTransaction.Note == null)
                {
                    Console.WriteLine("No note found for this transaction.");
                    Console.WriteLine("Enter a note for the transaction:");
                    string note = Console.ReadLine();
                    TransactionController.AddNoteToTransaction(selectedTransaction, note);
                    Console.WriteLine("Note added!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Note: " + selectedTransaction.Note);
                    Console.ReadKey();
                }
                break;
            case 3:
                Run();
                break;
        }
    }
    
    public override void Run()
    {
        while (true)
        {
            FillTransactionMenu();
            int selectedIndex = _selectableMenu.Run();
            HandleSelection(selectedIndex);
        }
    }
    
}