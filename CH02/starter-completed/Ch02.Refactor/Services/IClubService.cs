using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

/// <summary>[ĐÃ HOÀN THÀNH TODO 2 - SRP]: Tách nghiệp vụ CLB ra khỏi ClubManager.</summary>
public interface IClubService
{
    Club CreateClub(string name, string description, string presidentEmail);
    Member AddMember(Guid clubId, string memberName, string email);
}
