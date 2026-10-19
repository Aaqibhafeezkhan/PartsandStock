namespace PartsStockCLI;

public class Item
{
    public string ItemName { get; set; } = "";
    public int ItemNumber { get; set; }
    public string ItemDescription { get; set; } = "";
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemStock { get; set; }
    public string ItemPurchaseDate { get; set; } = "";
    public string StorageLocation { get; set; } = "";
    public string SourcingLocation { get; set; } = "";

    public override string ToString()
    {
        string storage = string.IsNullOrWhiteSpace(StorageLocation) ? "(unassigned)" : StorageLocation;
        string source = string.IsNullOrWhiteSpace(SourcingLocation) ? "(unassigned)" : SourcingLocation;

        return $"Item Name: {ItemName} | Item Number: {ItemNumber}\n" +
               $"  Description: {ItemDescription}\n" +
               $"  Price: {ItemPrice:F2} | Quantity: {ItemQuantity} | Stock: {ItemStock}\n" +
               $"  Purchased: {ItemPurchaseDate}\n" +
               $"  Storage: {storage}\n" +
               $"  Sourced From: {source}";
    }
}