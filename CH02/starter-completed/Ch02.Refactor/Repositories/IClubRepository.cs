using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

/// <summary>[ĐÃ HOÀN THÀNH TODO 2 - ISP]: Interface nhỏ cho dữ liệu CLB.</summary>
public interface IClubRepository
{
    IReadOnlyList<Club> GetAll();
    Club? GetById(Guid id);
    void Add(Club club);
}
