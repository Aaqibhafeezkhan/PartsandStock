using System.Text.Json.Serialization;
namespace PartsStockCLI;
// Single source of truth for all data models
public class Item
{
    public string ItemName { get; set; }
    public int ItemNumber { get; set; }
    public string ItemDescription { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemStock { get; set; }
    public string ItemPurchaseDate { get; set; }
    public string StorageLocation { get; set; }
    
    public override string ToString()
    {
        return $"Item Name: {ItemName} | Item Number: {ItemNumber}\n" +
               $"Description: {ItemDescription} | Price: ${ItemPrice}\n" +
               $"Quantity: {ItemQuantity} | Stock: {ItemStock}\n" +
               $"Purchase Date: {ItemPurchaseDate} | Location: {StorageLocation}";
    }
}

public class SourcingLocation
{
    public string LocationName { get; set; }
    public string LocationAddress { get; set; }
    public string LocationCity { get; set; }
    public string LocationState { get; set; }
    
    public override string ToString()
    {
        return $"{LocationName}\n{LocationAddress}\n{LocationCity}, {LocationState}";
    }
}

public class StorageLocation
{
    public int? Id { get; set; }
    public string LocationName { get; set; }
    public string Details { get; set; }
    public string LocationCity { get; set; }
    public string LocationType { get; set; }
    public int? ParentId { get; set; }
    
    [JsonIgnore]
    public StorageLocation Parent { get; set; }
    public List<StorageLocation> Children { get; set; }
    
    public StorageLocation()
    {
        Children = new List<StorageLocation>();
    }
    
    public override string ToString()
    {
        return $"{LocationName} (ID: {Id}) - {LocationType}, {LocationCity}";
    }
}