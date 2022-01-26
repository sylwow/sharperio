using Microsoft.AspNetCore.SignalR;

namespace SharperioBackend.WebUI.WebHubs;

public class UserNotificationHub : Hub
{
    public Task JoinNotification(string groupName)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public Task LeaveNotification(string groupName)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
