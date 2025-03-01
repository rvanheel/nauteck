using Azure.Storage.Blobs;

using nauteck.core.Abstraction;

namespace nauteck.core.Implementation;

public sealed class BlobStorage(BlobServiceClient blobServiceClient, string ContainerName) : IBlobStorage
{
    private readonly BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
    public Task DeleteBlob(string? logo, CancellationToken cancellationToken)
    {        
        if (string.IsNullOrWhiteSpace(logo)) return Task.CompletedTask;

        var fileName = Path.GetFileName(new Uri(logo).AbsolutePath);        
        return blobContainerClient.GetBlobClient(fileName).DeleteIfExistsAsync(cancellationToken: cancellationToken);

    }
}
