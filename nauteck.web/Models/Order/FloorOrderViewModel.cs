namespace nauteck.web.Models.Order;

public sealed record FloorOrderViewModel
{
    public required data.Entities.Order.FloorOrder FloorOrder { get; init; }
}
