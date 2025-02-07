using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using nauteck.data.Entities.Dealer;

namespace nauteck.persistence.Configuration;

public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
{
    public void Configure(EntityTypeBuilder<Dealer> builder)
    {
        builder.ToTable("dealer"); // Exacte naam uit DDL

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
               .HasColumnType("char(36)")
               .IsRequired();

        builder.Property(d => d.Name)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(d => d.Address)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.Number)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(d => d.Extension)
               .HasMaxLength(5)
               .IsRequired();

        builder.Property(d => d.Zipcode)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(d => d.City)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.Region)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.Country)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.Phone)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(d => d.Email)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(d => d.Active)
               .HasColumnType("tinyint(1)")
               .IsRequired();

        builder.Property(d => d.CreatedAt)
               .HasColumnType("datetime(6)")
               .IsRequired();

        builder.Property(d => d.CreatedBy)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.ModifiedAt)
               .HasColumnType("datetime(6)")
               .IsRequired();

        builder.Property(d => d.ModifiedBy)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.Description)
               .HasColumnType("longtext")
               .IsRequired();

        builder.Property(d => d.Provision)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0.00m)
               .IsRequired();

        builder.Property(d => d.FloorQuantityNotRequired)
               .HasColumnType("tinyint(1)")
               .HasDefaultValue(false)
               .IsRequired();

        // Relatie met Users (een dealer kan meerdere users hebben)
        builder.HasMany(d => d.Users)
               .WithOne(u => u.Dealer)
               .HasForeignKey(u => u.DealerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
