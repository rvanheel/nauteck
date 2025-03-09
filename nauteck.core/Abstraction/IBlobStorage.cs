namespace nauteck.core.Abstraction;

public interface IBlobStorage
{
    Task DeleteBlob(string? fileUrl, CancellationToken cancellationToken);
    Task DeleteBlobByFileName(string? fileName, CancellationToken cancellationToken);
}
