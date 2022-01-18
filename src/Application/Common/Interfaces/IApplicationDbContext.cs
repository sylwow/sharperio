using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<UserTables> UserTables { get; }
    DbSet<Table> Tables { get; }
    DbSet<Column> Columns { get; }
    DbSet<Item> Items { get; }
    DbSet<Label> Labels { get; }
    DbSet<Comment> Comments { get; }
    DbSet<Cover> Covers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
