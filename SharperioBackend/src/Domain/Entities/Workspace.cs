namespace SharperioBackend.Domain.Entities;

public class Workspace : SherableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<Table> Tables { get; set; }
}
