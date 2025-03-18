namespace nauteck.data.Models.Attachment;

public sealed record AttachmentPostModel
{
    public Uri? BlobUri { get; init; }
    public Guid ClientId { get; init; }
    public string? ContentType { get; init; }
    public string? FileName { get; init; }
    public long Size { get; init; }
}