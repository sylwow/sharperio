using Microsoft.AspNetCore.SignalR;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.WebUI.WebHubs;

namespace SharperioBackend.WebUI.Services;

public class UserNotificationService : IUserNotificationService
{
    private readonly IHubContext<UserNotificationHub> _notificationHub;

    public UserNotificationService(IHubContext<UserNotificationHub> notificationHub)
    {
        _notificationHub = notificationHub;
    }

    public async Task SendGroupNotification(string groupName, string method, object? data = null)
    {
        await _notificationHub.Clients.Group(groupName).SendAsync(method, data);
    }
}
