namespace EduHub.Ch02.Domain;

/// <summary>Câu lạc bộ — chứa quy tắc nghiệp vụ của chính nó, không chứa logic hạ tầng.</summary>
public class Club
{
    public const int MaxMembers = 50;
    public const int MaxNameLength = 100;

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    // [ĐÃ HOÀN THÀNH TODO 4]: Chuyển luật kiểm tra tên từ ClubManager.CreateClub vào đây.
    // Tên rỗng/toàn khoảng trắng -> ArgumentException; dài hơn MaxNameLength -> ArgumentException.
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name cannot be empty or whitespace.", nameof(name));

        if (name.Length > MaxNameLength)
            throw new ArgumentException(
                $"Club name cannot exceed {MaxNameLength} characters.",
                nameof(name));

        return new Club { Id = Guid.NewGuid(), Name = name, Description = description };
    }

    // [ĐÃ HOÀN THÀNH TODO 4]: Trả về true khi số thành viên hiện tại còn nhỏ hơn MaxMembers (50).
    public bool HasRoom(int currentMemberCount) => currentMemberCount < MaxMembers;
}
