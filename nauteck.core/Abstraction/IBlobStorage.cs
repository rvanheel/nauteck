namespace nauteck.core.Abstraction;

public interface IBlobStorage
{
    Task DeleteBlob(string? logo, CancellationToken cancellationToken);
}
