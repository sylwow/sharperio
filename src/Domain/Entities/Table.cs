
namespace CleanArchitecture.Domain.Entities;

public class Table : AuditableEntity
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public Cover Cover { get; set; }
    public bool IsPrivate { get; set; }
    public List<Column> Columns { get; set; } = new();
}
