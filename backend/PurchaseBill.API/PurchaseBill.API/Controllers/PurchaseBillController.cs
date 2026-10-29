using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseBill.API.DTOs;
using PurchaseBill.API.Services;
namespace PurchaseBill.API.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PurchaseBillController : ControllerBase {
    private readonly PurchaseBillService _service;
    public PurchaseBillController(PurchaseBillService service) => _service = service;
    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations() => Ok(await _service.GetLocationsAsync());
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PurchaseBillDto dto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdBy = User.Identity?.Name;
        return Ok(await _service.AddAsync(dto, createdBy));
    }
}
