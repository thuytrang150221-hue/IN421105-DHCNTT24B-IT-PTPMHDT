namespace EduHub.Ch02.Notifications;

/// <summary>
/// [ĐÃ HOÀN THÀNH TODO 3 - FACTORY PATTERN]:
/// Thay thế chuỗi if/else Gmail/Yahoo. Thêm kênh mới chỉ cần thêm case ở đây.
/// </summary>
public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotificationService(),
        NotificationChannel.Sms => new SmsNotificationService(),
        NotificationChannel.Slack => new SlackNotificationService(),
        _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unsupported channel")
    };
}
