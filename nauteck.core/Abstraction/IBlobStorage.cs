
namespace nauteck.core.Abstraction;

public interface IBlobStorage
{
    Task<Uri> BuildSasUrl(string fileName, int expiresInMinutes, CancellationToken cancellationToken);
    Task DeleteBlob(string? fileUrl, CancellationToken cancellationToken);
    Task DeleteBlobByFileName(string? fileName, CancellationToken cancellationToken);
}
