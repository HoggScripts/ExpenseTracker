namespace BudgetApp;

public static class MenuStack
{
    public static Stack<object> _menuStack = new Stack<object>();
    
    public static void Push(object menu)
    {
        _menuStack.Push(menu);
    }
    
    public static object Pop()
    {
        return _menuStack.Pop();
    }
    
    public static object Peek()
    {
        return _menuStack.Peek();
    }
}