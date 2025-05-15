using nauteck.data.Enums;

namespace nauteck.data.Entities.Account;

public sealed class User
{
    public Guid Id { get; set; }
    public Guid DealerId { get; set; } = Guid.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string Infix { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Preamble { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public RoleType Roles { get; set; }

    public bool Active { get; set; }


    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime ModifiedAt { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public Guid ActivationCode { get; set; } = Guid.Empty;
}
