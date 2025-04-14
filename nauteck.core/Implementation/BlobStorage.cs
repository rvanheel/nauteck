namespace nauteck.core.Implementation;

public sealed class BlobStorage(BlobServiceClient blobServiceClient, string containerName) : IBlobStorage
{
    private readonly BlobContainerClient _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

    public async Task<Uri> BuildSasUrl(string fileName, int expiresInMinutes, CancellationToken cancellationToken)
    {
        await _blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob, cancellationToken: cancellationToken);
        return _blobContainerClient.GetBlobClient(fileName)
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
        return _blobContainerClient.GetBlobClient(fileName).DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}
