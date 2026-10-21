using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

// ═══ Composition Root: Nơi DUY NHẤT dùng "new" cho class cụ thể (DIP) ═══
IAppLogger logger = new ConsoleLogger();

// Factory Pattern: Chọn kênh thông báo bằng enum
INotificationServiceFactory notificationFactory = new NotificationServiceFactory();
INotificationService notifier = notificationFactory.Create(NotificationChannel.Email);

// Decorator Pattern: Bọc repository gốc để tự động ghi log truy cập
IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository memberRepo = new InMemoryMemberRepository();
IEventRepository eventRepo = new InMemoryEventRepository();

// Tách Service (SRP & DIP): Tiêm phụ thuộc qua Constructor
IClubService clubService = new ClubService(clubRepo, memberRepo, notifier, logger);
IEventService eventService = new EventService(clubRepo, memberRepo, eventRepo);
IReportService reportService = new ReportService(clubRepo, memberRepo, eventRepo);

// ═══ 1. Demo Service: Tạo CLB và thêm thành viên ═══
Console.WriteLine("== 1. Tạo CLB và thêm thành viên ==");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
var a = clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
var b = clubService.AddMember(club.Id, "Trần Thị B", "b@student.dthu.edu.vn");

// ═══ 2. Demo Observer Pattern: Đăng ký 2 người nghe, khi có sự kiện thì cả 2 cùng nhận tin ═══
Console.WriteLine("\n== 2. Observer: tạo sự kiện -> 2 observer được báo ==");
eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, notifier));
var ev = eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));
eventService.Register(ev.Id, a.Id);
eventService.Register(ev.Id, b.Id);
eventService.CheckIn(ev.Id, a.Id);

// ═══ 3. Demo Strategy Pattern: Cùng dữ liệu nhưng xuất 2 định dạng khác nhau ═══
Console.WriteLine("\n== 3. Strategy: báo cáo Console ==");
IReportStrategy console = new ConsoleReportStrategy();
Console.Write(reportService.Generate(console));

Console.WriteLine("== 3. Strategy: báo cáo CSV ==");
IReportStrategy csv = new CsvReportStrategy();
Console.Write(reportService.Generate(csv));

// ═══ 4. Demo Factory Pattern: Đổi kênh gửi tin chỉ bằng tham số enum ═══
Console.WriteLine("\n== 4. Factory: tạo kênh thông báo theo yêu cầu ==");
foreach (var channel in Enum.GetValues<NotificationChannel>())
    notificationFactory.Create(channel).Send("a@student.dthu.edu.vn", $"Thử kênh {channel}");

// ═══ 5. Kiểm tra lỗi nghiệp vụ (Validation trong Domain) ═══
Console.WriteLine("\n== 5. Kiểm tra validate ==");
try
{
    clubService.CreateClub("", "mô tả", "p@gmail.com");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Bắt lỗi hợp lệ: {ex.Message}");
}
