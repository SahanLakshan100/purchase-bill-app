namespace PurchaseBill.API.Models;
public class AppUser { public int Id { get; set; } public string Email { get; set; } = string.Empty; public string DisplayName { get; set; } = string.Empty; public DateTime LastLoginAt { get; set; } = DateTime.UtcNow; public int LoginCount { get; set; } = 1; public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
