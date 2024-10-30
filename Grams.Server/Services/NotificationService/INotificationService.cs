namespace Grams.Server.Services.NotificationService;

public interface INotificationService
{
    Task<ServiceResponse<List<Notification>>> GetNotifications(int userId);
    Task<ServiceResponse<bool>> SendNotification(int userId, Notification notification);
}
