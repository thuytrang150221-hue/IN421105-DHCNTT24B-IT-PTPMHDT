
using EDUHUB_CH01.Domain;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

static void Step(int number, string title)
{
    Console.WriteLine();
    Console.WriteLine("========================================");
    Console.WriteLine($"Bước {number}: {title}");
    Console.WriteLine("========================================");
}

// BƯỚC 1: NHẬP THÔNG TIN CLB
Step(1, "Tạo câu lạc bộ");

Club club;

while (true)
{
    string clubName = ReadRequired("Nhập tên CLB: ");
    string description = ReadRequired("Nhập mô tả CLB: ");

    try
    {
        club = Club.Create(clubName, description);
        break;
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Dữ liệu không hợp lệ: {ex.Message}");
        Console.WriteLine("Vui lòng nhập lại thông tin CLB.");
    }
}

Console.WriteLine($"Đã tạo CLB: {club.Name}");

// BƯỚC 2: NHẬP THÔNG TIN THÀNH VIÊN
Step(2, "Thêm thành viên");

int memberCount = ReadInt("Bạn muốn nhập bao nhiêu sinh viên? ", 1, 100);

var members = new List<Member>();

for (int i = 0; i < memberCount; i++)
{
    Console.WriteLine($"\nNhập thông tin sinh viên thứ {i + 1}:");

    while (true)
    {
        string name = ReadRequired("Họ tên: ");
        string email = ReadRequired("Email: ");
        string studentCode = ReadRequired("MSSV: ");

        try
        {
            var member = Member.Create(name, email, studentCode);
            club.AddMember(member);
            members.Add(member);

            Console.WriteLine($"Đã thêm sinh viên: {member}");
            break;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Dữ liệu không hợp lệ: {ex.Message}");
            Console.WriteLine("Vui lòng nhập lại sinh viên này.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Không thể thêm thành viên: {ex.Message}");
            Console.WriteLine("Vui lòng kiểm tra dữ liệu và thử lại.");
        }
    }
}

Console.WriteLine($"Tổng thành viên CLB: {club.MemberCount()}");

// BƯỚC 3: NHẬP THÔNG TIN SỰ KIỆN
Step(3, "Tạo sự kiện");

Event workshop;

while (true)
{
    string title = ReadRequired("Tên sự kiện: ");
    DateTime eventDate = ReadDateTime(
        "Thời gian tổ chức (dd/MM/yyyy HH:mm): ");

    int capacity = ReadInt("Sức chứa sự kiện: ", 1, 10000);

    try
    {
        workshop = Event.Create(
            club.Id,
            title,
            eventDate,
            capacity
        );

        break;
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Dữ liệu không hợp lệ: {ex.Message}");
        Console.WriteLine("Vui lòng nhập lại thông tin sự kiện.");
    }
}

Console.WriteLine($"Đã tạo sự kiện: {workshop.Title}");
Console.WriteLine($"Thời gian: {workshop.Date:dd/MM/yyyy HH:mm}");
Console.WriteLine($"Sức chứa: {workshop.Capacity}");

// BƯỚC 4: CHỌN SINH VIÊN ĐĂNG KÝ
Step(4, "Đăng ký tham dự sự kiện");

var registrations = new List<Registration>();

for (int i = 0; i < members.Count; i++)
{
    Console.WriteLine($"\n{i + 1}. {members[i].Name} - {members[i].StudentCode}");

    string answer = ReadYesNo("Sinh viên này đăng ký? (Y/N): ");

    if (answer == "N")
        continue;

    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine("Sự kiện đã hết chỗ, không thể đăng ký.");
        continue;
    }

    var registration = Registration.Create(
        members[i].Id,
        workshop.Id
    );

    registrations.Add(registration);

    Console.WriteLine($"Đăng ký thành công. Trạng thái: {registration.Status}");
}

Console.WriteLine($"\nTổng số đăng ký: {registrations.Count}");

// BƯỚC 5: TỰ ĐỘNG XÁC NHẬN ĐĂNG KÝ
Step(5, "Xác nhận đăng ký");

foreach (var registration in registrations)
{
    registration.Confirm();
    Console.WriteLine($"Đã xác nhận phiếu: {registration.Id}");
}

// BƯỚC 6: CHỌN SINH VIÊN CHECK-IN
Step(6, "Check-in sự kiện");

var checkIns = new List<CheckIn>();

var confirmedRegistrations = registrations
    .Where(r => r.Status.ToString() == "Confirmed")
    .ToList();

if (confirmedRegistrations.Count == 0)
{
    Console.WriteLine("Không có đăng ký đã xác nhận để check-in.");
}
else
{
    Console.WriteLine("Danh sách đăng ký đã xác nhận:");

    for (int i = 0; i < confirmedRegistrations.Count; i++)
    {
        var registration = confirmedRegistrations[i];
        var member = members.First(m => m.Id == registration.MemberId);

        Console.WriteLine(
            $"{i + 1}. {member.Name} - {member.StudentCode}");
    }

    int selected = ReadInt(
        "Chọn số thứ tự sinh viên check-in (0 = bỏ qua): ",
        0,
        confirmedRegistrations.Count
    );

    if (selected != 0)
    {
        var registration = confirmedRegistrations[selected - 1];

        var checkIn = CheckIn.Create(
            registration,
            CheckInMethod.QrCode
        );

        checkIns.Add(checkIn);

        var checkedInMember = members.First(
            m => m.Id == registration.MemberId);

        Console.WriteLine($"Check-in thành công: {checkedInMember.Name}");
        Console.WriteLine(checkIn.Summary());
    }
}

// BƯỚC 7: TỰ ĐỘNG IN BÁO CÁO
Step(7, "Báo cáo tham dự");

var memberById = members.ToDictionary(m => m.Id);

int totalRegistrations = registrations.Count;
int totalCheckIns = checkIns.Count;

Console.WriteLine($"CLB: {club.Name}");
Console.WriteLine($"Sự kiện: {workshop.Title}");
Console.WriteLine($"Thời gian: {workshop.Date:dd/MM/yyyy HH:mm}");
Console.WriteLine($"Sức chứa: {workshop.Capacity}");
Console.WriteLine($"Số đăng ký: {totalRegistrations}");
Console.WriteLine($"Số người đã check-in: {totalCheckIns}");
Console.WriteLine($"Số người chưa check-in: {totalRegistrations - totalCheckIns}");
Console.WriteLine($"Số chỗ còn lại: {workshop.Capacity - totalRegistrations}");

Console.WriteLine("\nDanh sách tham dự:");

foreach (var registration in registrations)
{
    var member = memberById[registration.MemberId];

    bool attended = checkIns.Any(
        c => c.RegistrationId == registration.Id);

    Console.WriteLine(
        $"- {member.Name} | MSSV: {member.StudentCode} | " +
        $"{(attended ? "Đã check-in" : "Chưa check-in")}");
}

Console.WriteLine("\nĐã hoàn thành chương trình demo EduHub.");


// CÁC HÀM NHẬP LIỆU DÙNG CHUNG

static string ReadRequired(string message)
{
    while (true)
    {
        Console.Write(message);
        string value = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(value))
            return value.Trim();

        Console.WriteLine("Không được để trống. Vui lòng nhập lại.");
    }
}

static int ReadInt(string message, int min, int max)
{
    while (true)
    {
        Console.Write(message);
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int value)
            && value >= min && value <= max)
        {
            return value;
        }

        Console.WriteLine($"Vui lòng nhập số từ {min} đến {max}.");
    }
}

static DateTime ReadDateTime(string message)
{
    while (true)
    {
        Console.Write(message);
        string input = Console.ReadLine() ?? "";

        if (DateTime.TryParseExact(
            input,
            "dd/MM/yyyy HH:mm",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime result))
        {
            return result;
        }

        Console.WriteLine(
            "Ngày giờ không hợp lệ. Ví dụ: 25/10/2026 14:00");
    }
}

static string ReadYesNo(string message)
{
    while (true)
    {
        Console.Write(message);
        string answer = (Console.ReadLine() ?? "")
            .Trim()
            .ToUpperInvariant();

        if (answer == "Y" || answer == "N")
            return answer;

        Console.WriteLine("Vui lòng nhập Y hoặc N.");
    }
}