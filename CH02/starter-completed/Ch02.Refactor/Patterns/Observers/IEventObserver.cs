using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Patterns.Observers;

/// <summary>[ĐÃ HOÀN THÀNH TODO 3 - OBSERVER PATTERN]: Interface cho các bên lắng nghe sự kiện.</summary>
public interface IEventObserver
{
    void OnEventCreated(Event ev, Club club);
}
