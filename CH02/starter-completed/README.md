# So sánh Starter Gốc và Starter-Completed (Đã Refactor)

Thư mục này (`starter-completed`) được đặt cùng cấp với `starter` để dễ dàng so sánh từng bước refactor tương ứng với từng `// TODO`.

---

## 📊 Bảng so sánh trực quan từng TODO

| Mục TODO | Thư mục `starter` (Cũ) | Thư mục `starter-completed` (Mới) | Thay đổi & Ý nghĩa |
|---|---|---|---|
| **TODO 1** (Vi phạm SOLID) | Chưa có file phân tích | `docs/solid-analysis.md` | Ghi lại chi tiết 4 vi phạm SOLID (SRP, OCP, DIP, ISP) trong `ClubManager.cs`. |
| **TODO 4** (`Domain/Club.cs`) | `Create` & `HasRoom` ném `NotImplementedException` | Đã hoàn thiện logic | Kiểm tra tên bắt buộc, không quá 100 ký tự; kiểm tra sĩ số tối đa 50. |
| **TODO 4** (`Domain/Event.cs`) | `Register` & `CheckIn` ném `NotImplementedException` | Đã hoàn thiện logic | Chỉ cho phép check-in nếu đã đăng ký (`HashSet`); bổ sung `AttendanceRate`. |
| **TODO 4** (`DomainTests.cs`) | 6 test case bị `Skip` | Đã bỏ `Skip` và viết test AAA | 100% test xanh (Passed), kiểm tra cả trường hợp đúng lẫn ngoại lệ. |
| **TODO 2** (Tách God Class) | `ClubManager` ôm đồm mọi việc | Tách thành các Interface & Service riêng | `IClubService`, `IEventService`, `IReportService`, `IAppLogger`, các Repositories. |
| **TODO 3** (Factory) | Chuỗi `if/else` kiểm tra Gmail/Yahoo trong `ClubManager` | `Notifications/NotificationServiceFactory.cs` | Dùng Factory tạo dịch vụ `Email`, `Sms`, `Slack` theo Enum `NotificationChannel`. |
| **TODO 3** (Decorator) | Chưa có cơ chế ghi log khi truy cập Repository | `Repositories/LoggingClubRepository.cs` | Bọc `IClubRepository` gốc để tự động ghi log trước khi thao tác dữ liệu. |
| **TODO 3** (Strategy) | Hàm `PrintClubReport` in cứng ra Console | `Patterns/Strategies/` | Tách ra `IReportStrategy` với `ConsoleReportStrategy` và `CsvReportStrategy`. |
| **TODO 3** (Observer) | Hàm `CreateEvent` tự gửi email cho thành viên | `Patterns/Observers/` | `EventService` đóng vai Subject; `LogNotifier` và `EmailNotifier` tự nhận sự kiện. |
| **Entry Point** | Thiếu file `Program.cs` gây lỗi `CS5001` | Đã bổ sung `Program.cs` | Composition Root: nơi duy nhất ráp nối các phụ thuộc và demo 5 khối kịch bản. |

---

## 🛠️ Cách chạy thử dự án hoàn thiện

Mở Terminal và gõ:
```powershell
cd starter-completed
dotnet test Ch02.Refactor.sln
dotnet run --project Ch02.Refactor
```
