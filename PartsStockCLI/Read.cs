namespace PartsStockCLI;

public static class Read
{
    public static string Text(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    public static string RequiredText(string prompt)
    {
        while (true)
        {
            string value = Text(prompt).Trim();
            if (!string.IsNullOrWhiteSpace(value)) return value;
            Console.WriteLine("  A value is required.");
        }
    }

    public static int Int(string prompt, int min = 0)
    {
        while (true)
        {
            string raw = Text(prompt).Trim();
            if (int.TryParse(raw, out int value) && value >= min) return value;
            Console.WriteLine(min > 0
                ? $"  Enter a whole number of at least {min}."
                : "  Enter a whole number.");
        }
    }

    public static decimal Decimal(string prompt, decimal min = 0m)
    {
        while (true)
        {
            string raw = Text(prompt).Trim();
            if (decimal.TryParse(raw, out decimal value) && value >= min) return value;
            Console.WriteLine("  Enter a number, e.g. 12.50");
        }
    }

    public static bool Yes(string prompt)
    {
        string raw = Text(prompt).Trim().ToLowerInvariant();
        return raw == "y" || raw == "yes";
    }
}