namespace nauteck.data.Entities.Invoice;

public sealed class InvoiceLine
{
    public Guid Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    // navigation properties
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
}