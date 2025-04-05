namespace nauteck.data.Dto.Quotation;

public sealed record QuotationLineDto
{
    public Guid Id { get; init; }
    public decimal Quantity { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public Guid QuotationId { get; init; }
}