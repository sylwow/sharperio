using System.Drawing;

namespace SharperioBackend.Domain.Entities;

public class Item : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public int Order { get; set; }
    public bool IsArhived { get; set; } = false;
    public Cover? Cover { get; set; }
    public Column Column { get; set; }
    public List<Label> Labels { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
