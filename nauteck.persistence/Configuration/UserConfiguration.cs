using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using nauteck.data.Entities.Account;

namespace nauteck.persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user"); // MySQL-tabelnaam exact overnemen

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
               .HasColumnType("char(36)")
               .IsRequired();

        builder.Property(u => u.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.Infix)
               .HasMaxLength(10);

        builder.Property(u => u.LastName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.Preamble)
               .HasMaxLength(10);

        builder.Property(u => u.Email)
               .HasMaxLength(255)
               .IsRequired();

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.Property(u => u.Password)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(u => u.Description)
               .HasColumnType("longtext");

        builder.Property(u => u.Roles)
               .HasColumnType("tinyint unsigned")
               .IsRequired();

        builder.Property(u => u.Active)
               .HasColumnType("tinyint(1)")
               .IsRequired();

        builder.Property(u => u.DealerId)
               .HasColumnType("char(36)")
               .IsRequired();

        builder.HasIndex(u => u.DealerId);

        builder.Property(u => u.CreatedAt)
               .HasColumnType("datetime(6)")
               .IsRequired();

        builder.Property(u => u.CreatedBy)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.ModifiedAt)
               .HasColumnType("datetime(6)")
               .IsRequired();

        builder.Property(u => u.ModifiedBy)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.ActivationCode)
               .HasColumnType("char(36)")
               .HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'")
               .IsRequired();

        // Relatie met Dealer (Foreign Key)
        builder.HasOne(u => u.Dealer)
               .WithMany(d => d.Users)
               .HasForeignKey(u => u.DealerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
