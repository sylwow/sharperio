
namespace CleanArchitecture.Domain.Entities;

public class Table : AuditableEntity
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public Cover? Cover { get; set; }
    public bool IsPrivate { get; set; } = false;
    public List<Column> Columns { get; set; } = new();
    public List<string> UsersWithAccess { get; set; } = new();
}
