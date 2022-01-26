namespace SharperioBackend.Domain.Entities;

public class Column : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsArhived { get; set; } = false;
    public int Order { get; set; }
    public Table Table { get; set; }
    public List<Item> Items { get; set; } = new();
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
