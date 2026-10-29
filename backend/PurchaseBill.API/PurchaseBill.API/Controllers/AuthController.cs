using Microsoft.AspNetCore.Mvc;
using PurchaseBill.API.DTOs;
using PurchaseBill.API.Services;
namespace PurchaseBill.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly AuthService _auth;
    public AuthController(AuthService auth) => _auth = auth;
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _auth.LoginAsync(req);
        return result.Success ? Ok(result) : Unauthorized(result);
    }
}
