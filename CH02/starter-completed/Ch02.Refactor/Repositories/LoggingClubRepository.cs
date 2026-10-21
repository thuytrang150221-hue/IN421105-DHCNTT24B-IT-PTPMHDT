using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Repositories;

/// <summary>
/// [ĐÃ HOÀN THÀNH TODO 3 - DECORATOR PATTERN]:
/// Bọc ngoài một IClubRepository để ghi log truy cập mà không cần sửa code của InMemoryClubRepository.
/// </summary>
public class LoggingClubRepository : IClubRepository
{
    private readonly IClubRepository _inner;
    private readonly IAppLogger _logger;

    public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public IReadOnlyList<Club> GetAll()
    {
        _logger.Info("[repo] GetAll clubs");
        return _inner.GetAll();
    }

    public Club? GetById(Guid id)
    {
        _logger.Info($"[repo] GetById {id}");
        return _inner.GetById(id);
    }

    public void Add(Club club)
    {
        _logger.Info($"[repo] Add club '{club.Name}'");
        _inner.Add(club);
    }
}
