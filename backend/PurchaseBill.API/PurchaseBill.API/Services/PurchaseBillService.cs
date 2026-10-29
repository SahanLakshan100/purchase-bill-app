using Microsoft.EntityFrameworkCore;
using PurchaseBill.API.Data;
using PurchaseBill.API.DTOs;
using PurchaseBill.API.Models;
namespace PurchaseBill.API.Services;
public class PurchaseBillService {
    private readonly AppDbContext _db;
    public PurchaseBillService(AppDbContext db) => _db = db;
    public async Task<List<object>> GetLocationsAsync() => await _db.Location_Details.OrderBy(x => x.Location_Name).Select(x => new { x.Location_Code, x.Location_Name }).Cast<object>().ToListAsync();
    public async Task<object> AddAsync(PurchaseBillDto dto, string? createdBy) {
        var gross = dto.StandardCost * dto.Qty; var disc = gross * dto.DiscountPct / 100m;
        var entity = new Models.PurchaseBillEntity { ItemName = dto.ItemName, Batch = dto.Batch, StandardCost = dto.StandardCost, StandardPrice = dto.StandardPrice, DiscountPct = dto.DiscountPct, Qty = dto.Qty, TotalCost = gross - disc, TotalSelling = dto.StandardPrice * dto.Qty, CreatedByEmail = createdBy };
        _db.Purchase_Bills.Add(entity); await _db.SaveChangesAsync();
        return new { entity.Id, entity.TotalCost, entity.TotalSelling };
    }
}


