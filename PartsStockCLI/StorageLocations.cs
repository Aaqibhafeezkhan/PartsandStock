namespace PartsStockCLI;

public class StorageLocations
{
    private static readonly string Path_ = JsonStore.DocumentsPath("StorageLocations.json");

    public void StorageMain()
    {
        Console.Clear();
        Console.WriteLine("Storage Menu");
        Console.WriteLine("  1) Load Storage Location");
        Console.WriteLine("  2) Edit Storage Location");
        Console.WriteLine("  3) Create New Storage Location");
        Console.WriteLine("  4) Back");

        string choice = Read.Text("Choice: ").Trim();
        switch (choice)
        {
            case "1": LoadLocationMenu(); break;
            case "2": Console.WriteLine("Not implemented yet."); break;
            case "3": CreateLocation(); break;
            case "4": return;
            default: Console.WriteLine("Invalid input."); break;
        }
    }

    

    private static void PrintAll()
    {
        List<StorageLocation> locs = JsonStore.Load<StorageLocation>(Path_);
        if (locs.Count == 0)
        {
            Console.WriteLine("No storage locations defined.");
            return;
        }

        foreach (StorageLocation loc in locs) PrintLocation(loc, 0);
    }

    private static void PrintLocation(StorageLocation loc, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}[{loc.Id}] {loc.LocationName} ({loc.LocationType})");

        if (!string.IsNullOrWhiteSpace(loc.Details))
            Console.WriteLine($"{indent}    {loc.Details}");
        if (!string.IsNullOrWhiteSpace(loc.LocationCity))
            Console.WriteLine($"{indent}    {loc.LocationCity}");

        foreach (StorageLocation child in loc.Children)
            PrintLocation(child, depth + 1);
    }

    private static void SearchByName()
    {
        string term = Read.RequiredText("Enter the name to search by: ");
        List<StorageLocation> locs = JsonStore.Load<StorageLocation>(Path_);

        List<StorageLocation> matches = new List<StorageLocation>();
        foreach (StorageLocation loc in locs) CollectMatches(loc, term, matches);

        if (matches.Count == 0)
        {
            Console.WriteLine("No matches found.");
            return;
        }

        foreach (StorageLocation match in matches)
        {
            PrintLocation(match, 0);
            Console.WriteLine("--------");
        }
    }

    private static void CollectMatches(StorageLocation loc, string term, List<StorageLocation> into)
    {
        if (loc.LocationName != null &&
            loc.LocationName.Contains(term, StringComparison.OrdinalIgnoreCase))
        {
            into.Add(loc);
        }

        foreach (StorageLocation child in loc.Children)
            CollectMatches(child, term, into);
    }

    private static void CreateLocation()
    {
        Console.Clear();
        List<StorageLocation> locs = JsonStore.Load<StorageLocation>(Path_);

        int nextId = locs.Count == 0 ? 1 : locs.Max(l => l.Id ?? 0) + 1;

        StorageLocation loc = new StorageLocation
        {
            Id           = nextId++,
            ParentId     = null,
            LocationName = Read.RequiredText("Location Name: "),
            Details      = Read.Text("Details: "),
            LocationCity = Read.Text("Location City: "),
            LocationType = Read.Text("Location Type: ")
        };

        if (Read.Yes("Does this location have any sublocations? (y/N): "))
        {
            int subCount = Read.Int("Sublocation count: ", min: 1);
            for (int i = 0; i < subCount; i++)
            {
                Console.WriteLine($"  -- Sublocation {i + 1} --");
                loc.Children.Add(new StorageLocation
                {
                    Id           = nextId++,
                    ParentId     = loc.Id,
                    LocationName = Read.RequiredText("  Name: "),
                    Details      = Read.Text("  Details: "),
                    
                });
            }
        }

        locs.Add(loc);
        JsonStore.Save(Path_, locs);
        Console.WriteLine($"Storage location '{loc.LocationName}' created (Id {loc.Id}).");
    }
}

public class StorageLocation
{
    private void ViewItemsByStorageLocation()
    {
        string itemsPath = JsonStore.DocumentsPath("ItemList.json");
    
        Console.WriteLine("Enter storage location name: ");
        string locationName = Read.Text("");

        var items = JsonStore.Load<Item>(itemsPath);
        var itemsAtLocation = items.Where(i => 
            !string.IsNullOrEmpty(i.StorageLocation) &&
            i.StorageLocation.Contains(locationName, StringComparison.OrdinalIgnoreCase)).ToList();

        if (itemsAtLocation.Count == 0)
        {
            Console.WriteLine($"No items found at location '{locationName}'.");
            return;
        }

        Console.WriteLine($"\n=== Items at '{locationName}' ({itemsAtLocation.Count}) ===\n");
        foreach (var item in itemsAtLocation)
        {
            Console.WriteLine(item.ToString());
            Console.WriteLine("---");
        }
    }
    public int? Id { get; set; }
    public int? ParentId { get; set; }
    public string LocationName { get; set; } = "";
    public string Details { get; set; } = "";
    public string LocationCity { get; set; } = "";
    public string LocationType { get; set; } = "";
    public List<StorageLocation> Children { get; set; } = new List<StorageLocation>();
}