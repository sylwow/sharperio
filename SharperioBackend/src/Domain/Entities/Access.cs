namespace SharperioBackend.Domain.Entities;

public class Access
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<Table> Tables { get; set; } = new();
}
