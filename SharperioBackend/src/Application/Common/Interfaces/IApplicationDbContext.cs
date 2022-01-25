using SharperioBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SharperioBackend.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Access> Accesses { get; }
    DbSet<Workspace> Workspaces { get; }
    DbSet<Table> Tables { get; }
    DbSet<Column> Columns { get; }
    DbSet<Item> Items { get; }
    DbSet<Label> Labels { get; }
    DbSet<Comment> Comments { get; }
    DbSet<Cover> Covers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
