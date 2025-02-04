using System.ComponentModel.DataAnnotations;

namespace nauteck.data.Enums;

[Flags]
public enum RoleType : byte
{
    [Display(Name = nameof(Gebruiker))]
    Gebruiker = 1,

    [Display(Name = nameof(Editor))]
    Editor = 2,

    [Display(Name = nameof(Administrator))]
    Administrator = 4,

    [Display(Name = nameof(SuperUser))]
    SuperUser = 8
}
