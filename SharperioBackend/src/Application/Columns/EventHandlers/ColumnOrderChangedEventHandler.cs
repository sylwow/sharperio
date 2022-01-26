using SharperioBackend.Application.Common.Models;
using SharperioBackend.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using SharperioBackend.Application.Common.Interfaces;

namespace SharperioBackend.Application.Columns.EventHandlers;

public class ColumnOrderChangedEventHandler : INotificationHandler<DomainEventNotification<ColumnOrderChangedEvent>>
{
    private readonly ILogger<ColumnOrderChangedEventHandler> _logger;
    private readonly IUserNotificationService _userNotificationService;

    public ColumnOrderChangedEventHandler(ILogger<ColumnOrderChangedEventHandler> logger, IUserNotificationService userNotificationService)
    {
        _logger = logger;
        _userNotificationService = userNotificationService;
    }

    public Task Handle(DomainEventNotification<ColumnOrderChangedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("SharperioBackend Domain Event: {DomainEvent} {event}", domainEvent.GetType().Name, domainEvent);

        return _userNotificationService.SendGroupNotification(domainEvent.TableId.ToString(), domainEvent.GetType().Name, domainEvent);
    }
}
