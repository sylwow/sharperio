namespace SharperioBackend.Domain.Entities;

public class Workspace : OwnableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsDefault { get; set; }
    public List<Table> Tables { get; set; }
    public List<Access> Accesses { get; set; }
}
