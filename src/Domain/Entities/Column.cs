namespace CleanArchitecture.Domain.Entities;

public class Column : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsArhived { get; set; }
    public int Order { get; set; }
    public List<Item> Items { get; set; } = new();
    public Table Table { get; set; }
}
