using nauteck.data.Entities.Order;

namespace nauteck.data.Models.Order;

public sealed class OrderPostModel
{
    public string? Id { get; set; }
    public string? Reference { get; set; }
    public string? CompanyName { get; init; }
    public string? VatNumber { get; init; }
    // personalia
    public string? FirstName { get; init; } 
    public string? Infix { get; init; } 
    public string? LastName { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
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

    public string? Comment { get; init; }

    public decimal Discount { get; init; }

    public string? InvoiceFreeText { get; init; }
    public string? FreeText { get; init; }
    public decimal FreePrice { get; init; }
    public string? FreePriceText { get; init; }

    public decimal Provision { get; set; }
    public decimal ProvisionPercentage { get; set; }
    public decimal Total { get; set; }
    public string? CurrentStatus { get; init; }
    public string? Status { get; init; }
    public string? StatusAction { get; init; }
    public string? ConstructionBy { get; init; }

    public FloorOrderLogo?[] Logo { get; init; } = [];
    public FloorOrderPart? Parts { get; init; } = null!;

}
