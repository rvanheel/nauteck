using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nauteck.data.Entities.Floor.Color;

namespace nauteck.persistence.Configuration;

public class FloorColorExclusiveConfiguration : IEntityTypeConfiguration<FloorColorExclusive>
{
    public void Configure(EntityTypeBuilder<FloorColorExclusive> builder)
    {
        builder.ToTable("floorcolorexclusive");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnType("char(36)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Comment)
            .HasColumnType("longtext")
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ModifiedAt)
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Active)
            .HasColumnType("tinyint(1)")
            .IsRequired();
    }
}
