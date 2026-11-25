# Ch03 — Clean Architecture + REST API: EduHub Skeleton

## 1. Giới thiệu

Bài tập Ch03 xây dựng ứng dụng EduHub theo kiến trúc Clean Architecture, kết hợp REST API để quản lý câu lạc bộ (Club).

Dự án được chia thành các tầng riêng biệt nhằm phân tách trách nhiệm, giúp mã nguồn dễ quản lý, kiểm thử và mở rộng.

## 2. Cấu trúc dự án

- **EduHub.Domain:** Chứa các Entity và Repository Interface.
- **EduHub.Application:** Chứa các Use Case và Handler xử lý nghiệp vụ.
- **EduHub.Infrastructure:** Cài đặt Repository lưu trữ dữ liệu trong bộ nhớ (InMemory).
- **EduHub.API:** Xây dựng REST API bằng Controller, cấu hình Dependency Injection và Swagger.
- **EduHub.Tests:** Kiểm thử các chức năng của tầng Application bằng xUnit và Moq.

## 3. Các nội dung đã hoàn thành

### 3.1. Repository quản lý câu lạc bộ

Hoàn thành 5 phương thức trong `InMemoryClubRepository.cs`:

- Lấy danh sách câu lạc bộ.
- Lấy câu lạc bộ theo Id.
- Thêm câu lạc bộ mới.
- Cập nhật thông tin câu lạc bộ.
- Xóa câu lạc bộ.

### 3.2. Xử lý nghiệp vụ tại Application

Hoàn thành các Handler:

- `GetClubsQuery`: Lấy danh sách câu lạc bộ.
- `GetClubByIdQuery`: Lấy thông tin câu lạc bộ theo Id.
- `CreateClubCommand`: Kiểm tra dữ liệu đầu vào, tạo và lưu câu lạc bộ mới.
- `DeleteClubCommand`: Xử lý chức năng xóa câu lạc bộ.

### 3.3. Cấu hình API

Hoàn thành cấu hình trong `Program.cs`:

- Đăng ký Dependency Injection (DI).
- Đăng ký các Repository và Handler.
- Cấu hình Swagger để kiểm tra API.

### 3.4. Xây dựng REST API

Hoàn thành 4 endpoint trong `ClubsController`:

| Phương thức | Endpoint | Chức năng |
|---|---|---|
| GET | `/api/clubs` | Lấy danh sách câu lạc bộ |
| GET | `/api/clubs/{id}` | Lấy câu lạc bộ theo Id |
| POST | `/api/clubs` | Tạo câu lạc bộ mới |
| DELETE | `/api/clubs/{id}` | Xóa câu lạc bộ |

Các endpoint được xử lý với những HTTP Status Code phù hợp.

### 3.5. Kiểm thử Unit Test

- Giữ nguyên 4 test có sẵn.
- Bổ sung test mới cho chức năng lấy câu lạc bộ theo Id.
- Sử dụng Moq để giả lập Repository (Mock Repository).
- Kết quả kiểm thử: **5 tests passed**.

## 4. Cách chạy dự án

Mở Terminal tại thư mục `CH03/starter`, sau đó thực hiện:

```bash
dotnet build EduHub.sln
```

Chạy kiểm thử:

```bash
dotnet test
```

Chạy ứng dụng API:

```bash
dotnet run --project EduHub.API
```

Truy cập Swagger tại địa chỉ:

```text
http://localhost:5000/swagger
```

## 5. Kết quả

Đã hoàn thành các TODO từ 1 đến 8, bổ sung test thứ 5 bằng Moq và xây dựng các endpoint REST API quản lý câu lạc bộ.

Bài tập được thực hiện trên nhánh riêng `feature/ch03-skeleton` để chuẩn bị nộp Pull Request về nhánh `main`.

