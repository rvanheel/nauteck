namespace nauteck.data.Entities.Invoice;

public sealed class Invoice
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Reference { get; set; }

    public List<InvoiceLine> InvoiceLines { get; set; } = [];
}