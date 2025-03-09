namespace nauteck.data.Entities.Attachment;

public class Attachment
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = "";

    public string FileUrl { get; set; } = "";

    public long FileSize { get; set; }

    public string Type { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = "";

    public Guid ClientId { get; set; }
}

