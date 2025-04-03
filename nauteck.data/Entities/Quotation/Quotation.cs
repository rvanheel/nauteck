namespace nauteck.data.Entities.Quotation;

public sealed class Quotation
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Reference { get; set; }

    public List<QuotationLine> QuotationLines { get; set; } = [];
}
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