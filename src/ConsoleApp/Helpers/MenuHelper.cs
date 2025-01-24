namespace ConsoleApp.Helpers;

/// <summary>
/// Provides helper methods for displaying menus and getting user input.
/// </summary>
public static class MenuHelper
{
    /// <summary>
    /// Displays a menu with the given title and options.
    /// </summary>
    /// <param name="menuTitle">The title of the menu.</param>
    /// <param name="options">The list of options, each containing an option number, text, and action.</param>
    public static void ShowMenu(string menuTitle, List<(int OptionNumber, string Text, Action Action)> options)
    {
        Console.WriteLine($"{menuTitle}\n");

        foreach (var option in options)
        {
            Console.WriteLine($"{option.OptionNumber}. {option.Text}");
        }
        int choice = GetUserInputInt("\nMaak een keuze: ");
        var selectedOption = options.FirstOrDefault(option => option.OptionNumber == choice);

        if (selectedOption.Action != null)
        {
            selectedOption.Action();
        }
        else
        {
            Console.WriteLine("Ongeldige keuze");
        }

    }

    /// <summary>
    /// Prompts the user with a question and returns the input as a string.
    /// </summary>
    /// <param name="question">The question to ask the user.</param>
    /// <returns>The user's input as a string.</returns>
    public static string GetUserInput(string question)
    {

        string? input = "";

        Console.WriteLine(question);

        while (string.IsNullOrEmpty(input))
        {
            input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("ongeldige invoer. Probeer opnieuw: ");
            }

        }

        return input;

    }

    /// <summary>
    /// Prompts the user with a question and returns the input as an integer.
    /// </summary>
    /// <param name="question">The question to ask the user.</param>
    /// <returns>The user's input as an integer.</returns>
    public static int GetUserInputInt(string question)
    {

        string? input;

        Console.WriteLine(question);

        while (true)
        {
            input = Console.ReadLine()?.Trim();


            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ongeldige invoer. Voer een getal in: ");
            }

            else if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Dit is geen geldig getal. Probeer het opnieuw.");
            }
        }


    }

}