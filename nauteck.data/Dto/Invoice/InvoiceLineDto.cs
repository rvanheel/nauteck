namespace nauteck.data.Dto.Invoice;

public sealed record InvoiceLineDto
{
    public Guid Id { get; init; }
    public decimal Quantity { get; init; }
    public decimal Amount { get; init; }
    public decimal Tax { get; init; }
    public string? Description { get; init; }
    public Guid InvoiceId { get; init; }
}