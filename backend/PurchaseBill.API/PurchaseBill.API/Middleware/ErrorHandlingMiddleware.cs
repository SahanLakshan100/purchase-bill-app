using System.Net;
using System.Text.Json;
namespace PurchaseBill.API.Middleware;
public class ErrorHandlingMiddleware {
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next) { _next = next; }
    public async Task Invoke(HttpContext ctx) {
        try { await _next(ctx); } catch (Exception ex) {
            if (!ctx.Response.HasStarted) {
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ctx.Response.ContentType = "application/json";
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message }));
            }
        }
    }
}
