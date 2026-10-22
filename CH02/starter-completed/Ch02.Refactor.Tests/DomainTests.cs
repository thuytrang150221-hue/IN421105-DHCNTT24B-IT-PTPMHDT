using EduHub.Ch02;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: chạy được ngay trên ClubManager hiện tại.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var manager = new ClubManager();

        // Act
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // [ĐÃ HOÀN THÀNH TODO 4]: Bỏ Skip và viết thân test theo mẫu Arrange-Act-Assert.

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        const string name = "Tech Club";
        const string desc = "Câu lạc bộ công nghệ";

        // Act
        var club = Club.Create(name, desc);

        // Assert
        Assert.NotNull(club);
        Assert.Equal(name, club.Name);
        Assert.Equal(desc, club.Description);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Club_Create_WithInvalidName_ThrowsArgumentException(string blankName)
    {
        // Arrange & Act
        var act = () => Club.Create(blankName, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithNameLongerThan100_ThrowsArgumentException()
    {
        // Arrange
        var longName = new string('a', Club.MaxNameLength + 1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Club.Create(longName, "mô tả"));
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var result49 = club.HasRoom(49);
        var result50 = club.HasRoom(50);

        // Assert
        Assert.True(result49);
        Assert.False(result50);
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();

        // Act
        var added = ev.Register(memberId);

        // Assert
        Assert.True(added);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var addedAgain = ev.Register(memberId);

        // Assert
        Assert.False(addedAgain);
        Assert.Single(ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var unregisteredMemberId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => ev.CheckIn(unregisteredMemberId));
    }
}
