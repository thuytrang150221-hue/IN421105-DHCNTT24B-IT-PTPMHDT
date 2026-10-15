# User Stories — EduHub

## Sinh viên

### US-01 · Xem danh sách CLB
Là sinh viên, tôi muốn xem danh sách các câu lạc bộ, để tìm hiểu và lựa chọn CLB phù hợp.

- Given có CLB, When sinh viên chọn danh sách CLB, Then hệ thống hiển thị danh sách và cho phép xem thông tin chi tiết.
- Given chưa có CLB, When sinh viên xem danh sách, Then hệ thống hiển thị danh sách trống và thông báo chưa có câu lạc bộ.

### US-02 · Hủy đăng ký sự kiện
Là sinh viên, tôi muốn hủy đăng ký sự kiện đã đăng ký, để có thể thay đổi kế hoạch tham gia.

- Given đã đăng ký và được phép hủy, When sinh viên xác nhận hủy đăng ký, Then hệ thống cập nhật trạng thái và danh sách đăng ký.
- Given chưa đăng ký hoặc sự kiện không cho phép hủy, When sinh viên yêu cầu hủy, Then hệ thống từ chối và hiển thị thông báo phù hợp.

## Ban CLB

### US-03 · Chỉnh sửa thông tin sự kiện
Là thành viên Ban CLB, tôi muốn chỉnh sửa thông tin sự kiện, để cập nhật nội dung mới cho sinh viên.

- Given thông tin hợp lệ, When Ban CLB lưu thông tin mới, Then hệ thống cập nhật thông tin sự kiện.
- Given thông tin bắt buộc bị bỏ trống hoặc không hợp lệ, When Ban CLB lưu thông tin, Then hệ thống từ chối cập nhật và hiển thị thông báo lỗi.

### US-04 · Xem danh sách sinh viên đăng ký sự kiện
Là thành viên Ban CLB, tôi muốn xem danh sách sinh viên đăng ký sự kiện, để quản lý người tham gia.

- Given có sinh viên đăng ký, When Ban CLB chọn sự kiện và xem danh sách đăng ký, Then hệ thống hiển thị danh sách sinh viên và thông tin chi tiết.
- Given chưa có sinh viên đăng ký, When Ban CLB xem danh sách, Then hệ thống hiển thị danh sách trống và thông báo chưa có người đăng ký.

## Admin

### US-05 · Vô hiệu hóa tài khoản sinh viên
Là Admin, tôi muốn vô hiệu hóa tài khoản sinh viên, để quản lý tài khoản trên hệ thống.

- Given tài khoản đang hoạt động, When Admin xác nhận vô hiệu hóa, Then hệ thống chuyển tài khoản sang trạng thái bị khóa.
- Given tài khoản đã bị khóa, When Admin yêu cầu vô hiệu hóa lại, Then hệ thống không thực hiện và thông báo tài khoản hiện đang bị khóa.