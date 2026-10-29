using System.ComponentModel.DataAnnotations;
namespace PurchaseBill.API.DTOs;
public class PurchaseBillDto { [Required] public string ItemName { get; set; } = string.Empty; [Required] public string Batch { get; set; } = string.Empty; [Range(0, double.MaxValue)] public decimal StandardCost { get; set; } [Range(0, double.MaxValue)] public decimal StandardPrice { get; set; } [Range(0, 100)] public decimal DiscountPct { get; set; } [Range(1, int.MaxValue)] public int Qty { get; set; } }
