namespace EduHub.Ch02.Logging;

/// <summary>[ĐÃ HOÀN THÀNH TODO 2 - DIP]: Abstraction cho việc ghi log thay vì gọi trực tiếp File.AppendAllText.</summary>
public interface IAppLogger
{
    void Info(string message);
}
