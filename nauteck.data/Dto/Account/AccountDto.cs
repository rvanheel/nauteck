using nauteck.data.Enums;

namespace nauteck.data.Dto.Account;

public sealed record AccountDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? Infix { get; init; }
    public string? LastName { get; init; }
    public string? Preamble { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? Description { get; init; }
    public RoleType Roles { get; init; }
    public Guid DealerId { get; init; }
    public bool Active { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string? CreatedBy { get; init; }
    public DateTimeOffset ModifiedAt { get; init; }
    public string? ModifiedBy { get; init; }
    public string? FullName => $"{FirstName} {Infix} {LastName}";
}
