namespace nauteck.data.Entities.Quotation;

public sealed class QuotationLine
{
    public Guid Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    // navigation properties
    public Guid QuotationId { get; set; }
    public Quotation Quotation { get; set; } = null!;
}