namespace SharperioBackend.Domain.Common;

public abstract class SherableEntity : OwnableEntity
{
    public List<string> UsersWithAccess { get; set; }
}
