namespace PurchaseBill.API.DTOs;
public class LoginResponse { public bool Success { get; set; } public string? Token { get; set; } public List<LocationDto> User_Locations { get; set; } = new(); public string? Message { get; set; } }
public class LocationDto { public string Location_Code { get; set; } = string.Empty; public string Location_Name { get; set; } = string.Empty; }
