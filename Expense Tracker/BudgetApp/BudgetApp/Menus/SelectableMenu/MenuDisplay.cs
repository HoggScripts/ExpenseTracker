using Spectre.Console;

namespace BudgetApp;

public class MenuDisplay
{
    public void DisplayOptions(string prompt, string[] options, int selectedIndex)
    {
        Clear();

        var asciiArt = new FigletText("Budget App");
        AnsiConsole.Render(asciiArt.Color(Color.Green));
        
        AnsiConsole.MarkupLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            var currentOption = options[i];
            string prefix;

            if (i == selectedIndex)
            {
                prefix = "*";
            }
            else
            {
                prefix = " ";
            }
        
            AnsiConsole.MarkupLine($"{prefix} {currentOption} {prefix}");
        }
    }

    public void Clear()
    {
        Console.Clear();
    }
}