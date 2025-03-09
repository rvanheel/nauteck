namespace nauteck.data.Models.Client;

public sealed record ClientPostModel
{
    public Guid Id { get; set; }

    public string? Preamble { get; init; }

    public string LastName { get; set; } = "";

    public string? FirstName { get; set; }

    public string? Infix { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; } = "";

    public string Number { get; set; } = "";

    public string? Extension { get; set; }

    public string Zipcode { get; set; } = "";

    public string City { get; set; } = "";

    public string? Region { get; set; } = "";

    public string Country { get; set; } = "";

    public string BoatBrand { get; set; } = "";

    public string BoatType { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public string? Remarks { get; set; }
    public bool Active { get; set; }
}
