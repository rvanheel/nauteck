namespace nauteck.data.Models.Order;

public sealed class OrderPostModel
{
    public string? Id { get; init; } 
    public string? CompanyName { get; init; }
    public string? VatNumber { get; init; }
    // personalia
    public string? FirstName { get; init; } 
    public string? Infix { get; init; } 
    public string? LastName { get; init; } 
    public string? Preamble { get; init; } 

    // address
    public string? Address { get; init; } 
    public string? City { get; init; } 
    public string? Country { get; init; } 
    public string? Extension { get; init; } 
    public string? Number { get; init; } 
    public string? Region { get; init; } 
    public string? Zipcode { get; init; }

    // boat
    public string? BoatBrand { get; init; }
    public string? BoatType { get; init; }

}
