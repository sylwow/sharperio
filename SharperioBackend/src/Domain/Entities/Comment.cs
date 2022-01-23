namespace SharperioBackend.Domain.Entities;

public class Comment : AuditableEntity
{
    public int Id { get; set; }
    public string text { get; set; }
}
