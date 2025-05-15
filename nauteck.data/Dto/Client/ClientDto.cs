namespace nauteck.data.Dto.Client;

public sealed record ClientDto
{

    public Guid Id { get; init; }
    public string? Preamble { get; init; }
    public string LastName { get; init; } = "";
    public string? FirstName { get; init; }
    public string? Infix { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public string Address { get; init; } = "";
    public string Number { get; init; } = "";
    public string? Extension { get; init; }
    public string Zipcode { get; init; } = "";
    public string City { get; init; } = "";
    public string? Region { get; init; } = "";
    public string Country { get; init; } = "";
    public string BoatBrand { get; init; } = "";
    public string BoatType { get; init; } = "";
    public bool Active { get; init; }
    public DateTime CreatedAt { get; init; }
}
