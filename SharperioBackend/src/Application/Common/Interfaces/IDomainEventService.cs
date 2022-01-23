using SharperioBackend.Domain.Common;

namespace SharperioBackend.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
