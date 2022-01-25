
namespace SharperioBackend.Domain.Entities;

public class Table : OwnableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Cover? Cover { get; set; }
    public bool IsPrivate { get; set; } = false;
    public bool IsArhived { get; set; } = false;
    public Workspace Workspace { get; set; }
    public List<Access> Accesses { get; set; }
    public List<Column> Columns { get; set; } = new();
}
