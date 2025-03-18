using Azure.Storage.Blobs;

using nauteck.core.Abstraction;

namespace nauteck.core.Implementation;

public sealed class BlobStorage(BlobServiceClient blobServiceClient, string ContainerName) : IBlobStorage
{
    private readonly BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

    public async Task<Uri> BuildSasUrl(string fileName, int expiresInMinutes, CancellationToken cancellationToken)
    {
        await blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob, cancellationToken: cancellationToken);
        return blobContainerClient.GetBlobClient(fileName)
            .GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Write, DateTimeOffset.UtcNow.AddMinutes(expiresInMinutes));
    }

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
