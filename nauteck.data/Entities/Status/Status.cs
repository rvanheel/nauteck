namespace nauteck.data.Entities.Status;

public sealed class Status
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool Active { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
}
