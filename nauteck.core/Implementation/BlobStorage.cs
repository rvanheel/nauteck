using Azure.Storage.Blobs;

using nauteck.core.Abstraction;

namespace nauteck.core.Implementation;

public sealed class BlobStorage(BlobServiceClient blobServiceClient, string ContainerName) : IBlobStorage
{
    private readonly BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
    public Task DeleteBlob(string? fileUrl, CancellationToken cancellationToken)
    {        
        if (string.IsNullOrWhiteSpace(fileUrl)) return Task.CompletedTask;

        var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);        
        return DeleteBlobByFileName(fileName, cancellationToken);

    }

    public Task DeleteBlobByFileName(string? fileName, CancellationToken cancellationToken)
    {
        return blobContainerClient.GetBlobClient(fileName).DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}
