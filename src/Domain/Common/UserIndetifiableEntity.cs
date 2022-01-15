namespace CleanArchitecture.Domain.Common;

public abstract class UserIdentifiableEntity : AuditableEntity
{
    public string UserId { get; set; }
}
