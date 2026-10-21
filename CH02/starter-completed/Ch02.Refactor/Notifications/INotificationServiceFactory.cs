namespace EduHub.Ch02.Notifications;

/// <summary>[ĐÃ HOÀN THÀNH TODO 3 - FACTORY PATTERN]: Interface cho Factory tạo kênh thông báo.</summary>
public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}
