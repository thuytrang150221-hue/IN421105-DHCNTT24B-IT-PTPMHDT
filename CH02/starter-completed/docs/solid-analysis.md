# Báo cáo phân tích vi phạm SOLID trong ClubManager (TODO 1)

Tài liệu phục vụ **Nhiệm vụ 1**: Phân tích chi tiết ít nhất 4 vi phạm SOLID trong "God class" `ClubManager.cs`.

---

## 1. Vi phạm SRP (Single Responsibility Principle - Nguyên tắc đơn trách nhiệm)
- **Vị trí**: Các phương thức `CreateClub` (dòng 35), `AddMemberToClub` (dòng 67), `CreateEvent` (dòng 88), `PrintClubReport` (dòng 103).
- **Hiện tượng**: `ClubManager` ôm đồm quá nhiều vai trò khác nhau:
  1. Kiểm tra tính hợp lệ dữ liệu (validation logic).
  2. Lưu trữ danh sách đối tượng (in-memory state).
  3. Ghi nhật ký trực tiếp ra file text (`File.AppendAllText`).
  4. Gửi thông báo đến người dùng qua Console/Email/Slack.
  5. Xử lý logic sự kiện và tự format chuỗi báo cáo.
- **Hậu quả**: Khi bất kỳ yêu cầu nào thay đổi (đổi luật validation, đổi nơi lưu trữ, đổi kênh gửi tin, đổi cách format báo cáo), lập trình viên đều phải sửa `ClubManager`. Điều này khiến code dễ lỗi dây chuyền và không thể viết unit test độc lập.
- **Cách khắc phục**:
  - Chuyển logic kiểm tra về chính Domain Entity (`Club.Create`, `Club.HasRoom`, `Event.Register`, `Event.CheckIn`).
  - Tách chức năng lưu trữ thành các Repository riêng (`IClubRepository`, `IMemberRepository`, `IEventRepository`).
  - Tách chức năng ghi log sang interface `IAppLogger`.
  - Tách thông báo sang `INotificationService`.
  - Tách điều phối nghiệp vụ sang các Service riêng (`ClubService`, `EventService`, `ReportService`).

---

## 2. Vi phạm OCP (Open/Closed Principle - Nguyên tắc Đóng / Mở)
- **Vị trí**:
  - Chuỗi `if/else` kiểm tra đuôi email (`@gmail.com`, `@yahoo.com`) trong `CreateClub` (dòng 52-63).
  - Các lệnh in cố định email và Slack trong `AddMemberToClub` (dòng 82-83).
  - Định dạng text cố định trong `PrintClubReport` (dòng 103-113).
- **Hiện tượng**: Khi muốn hỗ trợ thêm kênh thông báo mới (như SMS, Zalo, Teams) hoặc muốn xuất báo cáo sang file CSV hay bảng HTML, lập trình viên buộc phải mở file `ClubManager.cs` ra để sửa mã nguồn hiện có.
- **Hậu quả**: Vi phạm quy tắc "Mở cho mở rộng, Đóng cho sửa đổi". Sửa đổi code cũ có nguy cơ cao làm hỏng các tính năng đang chạy ổn định.
- **Cách khắc phục**:
  - Dùng **Factory Pattern** (`NotificationServiceFactory`) kết hợp `INotificationService`. Muốn thêm kênh mới chỉ cần thêm class mới mà không đụng đến service.
  - Dùng **Strategy Pattern** (`IReportStrategy`) cho báo cáo. Cho phép bổ sung định dạng mới (`ConsoleReportStrategy`, `CsvReportStrategy`) một cách linh hoạt.

---

## 3. Vi phạm DIP (Dependency Inversion Principle - Nguyên tắc đảo ngược phụ thuộc)
- **Vị trí**:
  - Lệnh gọi trực tiếp I/O cấp thấp: `File.AppendAllText(_logFilePath, ...)` và `Console.WriteLine(...)`.
  - Quản lý phụ thuộc cứng: `private readonly List<Club> _clubs = new();` và `_members = new();`.
- **Hiện tượng**: Module cấp cao (`ClubManager`) bị phụ thuộc cứng vào các chi tiết hạ tầng cụ thể (hệ thống file NTFS/FAT, luồng xuất chuẩn Console, danh sách RAM cục bộ).
- **Hậu quả**: Không thể chuyển sang lưu cơ sở dữ liệu (SQL Server, SQLite, MongoDB) hay đổi sang thư viện logging chuyên nghiệp (Serilog, NLog) mà không phải viết lại toàn bộ class. Không thể viết Unit Test vì không mock được file system và Console.
- **Cách khắc phục**:
  - Đảo ngược phụ thuộc: các Service chỉ phụ thuộc vào Abstraction (`IAppLogger`, `IClubRepository`, `INotificationService`).
  - Sử dụng Constructor Injection để truyền các phụ thuộc vào Service tại Composition Root (`Program.cs`).

---

## 4. Vi phạm ISP (Interface Segregation Principle - Nguyên tắc phân tách giao diện)
- **Hiện tượng**: Gom chung tất cả các thao tác của Câu lạc bộ, Thành viên, Sự kiện và Báo cáo vào chung một giao diện lớn `ClubManager`.
- **Hậu quả**: Một module chỉ cần xem thông tin CLB vẫn phải chịu sự phụ thuộc của Event và Báo cáo. Khi một phần thay đổi, toàn bộ hệ thống phải compile và kiểm thử lại.
- **Cách khắc phục**:
  - Phân tách thành các interface nhỏ gọn, tập trung đúng nhiệm vụ: `IClubRepository`, `IMemberRepository`, `IEventRepository`.
