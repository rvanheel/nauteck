namespace nauteck.data.Entities.Order;

public sealed class FloorOrderLogo
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid FloorOrderId { get; set; }
    public string Description { get; set; } = "";    
}