using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

namespace EduHub.Ch02;

/// <summary>
/// [ĐÃ HOÀN THÀNH TODO 2]: ClubManager cũ đã được refactor thành các Service và Repository riêng biệt.
/// Class này được giữ lại dưới dạng Facade để tương thích với các bài test cũ gọi trực tiếp ClubManager.
/// </summary>
[Obsolete("Đã được refactor thành IClubService, IEventService, IReportService và các Repositories.")]
public class ClubManager
{
    private readonly IClubService _clubService;
    private readonly IClubRepository _clubRepo;

    public ClubManager()
    {
        _clubRepo = new InMemoryClubRepository();
        var memberRepo = new InMemoryMemberRepository();
        var logger = new ConsoleLogger();
        var notificationFactory = new NotificationServiceFactory();
        var notifier = notificationFactory.Create(NotificationChannel.Email);
        _clubService = new ClubService(_clubRepo, memberRepo, notifier, logger);
    }

    public void CreateClub(string name, string description, string presidentEmail)
    {
        _clubService.CreateClub(name, description, presidentEmail);
    }

    public IReadOnlyList<Club> GetAllClubs() => _clubRepo.GetAll();
}
