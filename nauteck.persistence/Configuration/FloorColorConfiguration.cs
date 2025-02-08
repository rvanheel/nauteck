using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using nauteck.data.Entities.Floor.Color;

namespace nauteck.persistence.Configuration;

public sealed class FloorColorConfiguration : IEntityTypeConfiguration<FloorColor>
{
    public void Configure(EntityTypeBuilder<FloorColor> builder)
    {
        builder.ToTable("floorcolor");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id)
            .IsRequired()
            .HasColumnType("char(36)")
            .IsFixedLength();

        builder.Property(sc => sc.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(sc => sc.Comment)
            .IsRequired()
            .HasColumnType("longtext");

        builder.Property(sc => sc.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime(6)");

        builder.Property(sc => sc.CreatedBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(sc => sc.ModifiedAt)
            .IsRequired()
            .HasColumnType("datetime(6)");

        builder.Property(sc => sc.ModifiedBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(sc => sc.Active)
            .IsRequired()
            .HasColumnType("tinyint(1)");
    }
}
