namespace PurchaseBill.API.Models;

public class PurchaseBillEntity
{
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Batch { get; set; } = string.Empty;
    public decimal StandardCost { get; set; }
    public decimal StandardPrice { get; set; }
    public decimal DiscountPct { get; set; }
    public int Qty { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalSelling { get; set; }
    public string? CreatedByEmail { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}