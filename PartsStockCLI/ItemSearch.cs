namespace PartsStockCLI;

public class ItemSearch
{
    private static readonly string Path_ = JsonStore.DocumentsPath("ItemList.json");

    public void Menu()
    {
        Console.Clear();
        Console.WriteLine("Search by:");
        Console.WriteLine("  1) Item Name");
        Console.WriteLine("  2) Item Number");
        Console.WriteLine("  3) Storage Location");
        Console.WriteLine("  4) Sourcing Location");
        Console.WriteLine("  5) Back");

        string choice = Read.Text("Choice: ").Trim();
        List<Item> results;

        switch (choice)
        {
            case "1": results = SearchByName(); break;
            case "2": results = SearchByNumber(); break;
            case "3": results = SearchByField("Storage location", i => i.StorageLocation); break;
            case "4": results = SearchByField("Sourcing location", i => i.SourcingLocation); break;
            case "5": return;
            default:
                Console.WriteLine("Invalid input.");
                return;
        }

        PrintResults(results);
    }

    private static List<Item> SearchByName()
    {
        string term = Read.RequiredText("Item name to search for: ");
        return JsonStore.Load<Item>(Path_)
            .Where(i => i.ItemName.Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private static List<Item> SearchByNumber()
    {
        int number = Read.Int("Enter item number: ", min: 1);
        return JsonStore.Load<Item>(Path_)
            .Where(i => i.ItemNumber == number)
            .ToList();
    }

    private static List<Item> SearchByField(string label, Func<Item, string> selector)
    {
        string term = Read.RequiredText($"{label} to search for: ");
        return JsonStore.Load<Item>(Path_)
            .Where(i => (selector(i) ?? "").Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private static void PrintResults(List<Item> results)
    {
        Console.WriteLine();
        if (results.Count == 0)
        {
            Console.WriteLine("No matches found.");
            return;
        }

        Console.WriteLine($"Results ({results.Count}):");
        Console.WriteLine("--------");
        foreach (Item item in results)
        {
            Console.WriteLine(item);
            Console.WriteLine("--------");
        }
    }
}