using SharperioBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharperioBackend.Infrastructure.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
