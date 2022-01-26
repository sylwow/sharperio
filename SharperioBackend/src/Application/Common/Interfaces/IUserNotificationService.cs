namespace SharperioBackend.Application.Common.Interfaces;

public interface IUserNotificationService
{
    Task SendGroupNotification(string groupName, string method, object? data = null);
}
