namespace SharperioBackend.Domain.Common;

public abstract class OwnableEntity : AuditableEntity
{
    public string OwnerId { get; set; }
}
