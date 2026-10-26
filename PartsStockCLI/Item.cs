namespace PartsStockCLI;

public class Item
{
    public string ItemName { get; set; }
    public int ItemNumber { get; set; }
    public string ItemDescription { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemStock { get; set; }  // Make sure this exists
    public string ItemPurchaseDate { get; set; }
    public string StorageLocation { get; set; }
    public string SourcingLocation { get; set; }  // New addition from your code
    
    public override string ToString()
    {
        return $"Item Name: {ItemName} | Item Number: {ItemNumber}\n" +
               $"Description: {ItemDescription} | Price: ${ItemPrice}\n" +
               $"Quantity: {ItemQuantity} | Stock: {ItemStock}\n" +
               $"Purchase Date: {ItemPurchaseDate}\n" +
               $"Storage: {StorageLocation} | Sourced From: {SourcingLocation}";
    }
}