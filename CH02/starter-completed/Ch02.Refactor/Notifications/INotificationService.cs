namespace EduHub.Ch02.Notifications;

/// <summary>[ĐÃ HOÀN THÀNH TODO 2]: Interface gửi thông báo, tuân thủ OCP.</summary>
public interface INotificationService
{
    void Send(string to, string message);
}
