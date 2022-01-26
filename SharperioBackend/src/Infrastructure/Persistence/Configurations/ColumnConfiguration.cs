using SharperioBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharperioBackend.Infrastructure.Persistence.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
