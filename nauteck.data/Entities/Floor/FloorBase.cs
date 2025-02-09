namespace nauteck.data.Entities.Floor;

public abstract class FloorBase
{
    public Guid Id { get; set; }
    public string Description { get; set; } = "";
    public string Comment { get; set; } = "";
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public bool Active { get; set; }
}