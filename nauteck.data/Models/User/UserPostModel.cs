using nauteck.data.Enums;

namespace nauteck.data.Models.User;

public sealed record UserPostModel
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? Infix { get; init; } = "";
    public string? LastName { get; init; } = "";
    public string? Preamble { get; init; } = "";
    public string? Email { get; init; } = "";
    public string? Password { get; set; } = "";
    public string? Description { get; init; } = "";
    public RoleType Roles { get; init; }
    public Guid DealerId { get; set; }
    public bool Active { get; init; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public Guid? ActivationCode { get; set; }
    public string PasswordHash { get; init; } = "";
}
