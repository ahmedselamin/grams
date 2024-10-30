
namespace Grams.Server.Services.NotificationService;

public class NotificationService : INotificationService
{
    private readonly DataContext _context;

    public NotificationService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<Notification>>> GetNotifications(int userId)
    {
        var response = new ServiceResponse<List<Notification>>();

        try
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Id)
                .ToListAsync();

            response.Data = notifications;
            response.Message = notifications.Any() ? "" : "No notifications";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<bool>> SendNotification(int userId, string message)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                CreatedAt = DateTime.Now,
            };

            await _context.Notifications.AddAsync(notification);

            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Notification sent";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
}
