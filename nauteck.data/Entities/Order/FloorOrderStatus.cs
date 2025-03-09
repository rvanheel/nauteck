namespace nauteck.data.Entities.Order;

public sealed class FloorOrderStatus
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public Guid FloorOrderId { get; set; }
}
