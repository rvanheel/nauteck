
namespace nauteck.data.Dto;

public sealed record QuotationDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string? Description { get; init; }
    public string? Status { get; init; }
    public string? Reference { get; init; }

    // client
    public string? Preamble { get; init; }
    public string? FirstName { get; init; }
    public string? Infix { get;init; }
    public string? LastName { get; init; }
    public string? Address { get; init; }
    public string? Number { get; init; }
    public string? Extension { get; init; }
    public string? Zipcode { get; init; }
    public string? City { get; init; }
    public string? Region { get; init; }
    public string? Country { get; init; }
}
