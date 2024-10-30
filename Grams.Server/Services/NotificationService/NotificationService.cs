
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
            if (!notifications.Any())
            {
                response.Success = false;
                response.Message = "No notifications";

                return response;
            }

            response.Data = notifications;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<bool>> SendNotification(int userId, Notification notification)
    {
        var response = new ServiceResponse<bool>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
}
