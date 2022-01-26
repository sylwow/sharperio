using SharperioBackend.Application.Common.Models;
using SharperioBackend.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using SharperioBackend.Application.Common.Interfaces;

namespace SharperioBackend.Application.Items.EventHandlers;

public class ItemOrderChangedEventHandler : INotificationHandler<DomainEventNotification<ItemOrderChangedEvent>>
{
    private readonly ILogger<ItemOrderChangedEventHandler> _logger;
    private readonly IUserNotificationService _userNotificationService;

    public ItemOrderChangedEventHandler(ILogger<ItemOrderChangedEventHandler> logger, IUserNotificationService userNotificationService)
    {
        _logger = logger;
        _userNotificationService = userNotificationService;
    }

    public Task Handle(DomainEventNotification<ItemOrderChangedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("SharperioBackend Domain Event: {DomainEvent} {event}", domainEvent.GetType().Name, domainEvent);

        return _userNotificationService.SendGroupNotification(domainEvent.TableId.ToString(), domainEvent.GetType().Name, domainEvent);
    }
}
