namespace ConsoleApp.Helpers;

//static betekend aan te roepen vanuit class zonder instantie hoeven aan te maken
public static class MenuHelper
{

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