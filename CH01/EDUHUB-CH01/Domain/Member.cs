using System;
using System.Collections.Generic;
using System.Text;

namespace EDUHUB_CH01.Domain
{
    /// <summary>Trạng thái hoạt động của thành viên.</summary>
    public enum MemberStatus
    {
        /// <summary>Đang hoạt động.</summary>
        Active,
        /// <summary>Đã ngừng hoạt động.</summary>
        Inactive
    }

    /// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
    public class Member
    {
        /// <summary>Định danh duy nhất.</summary>
        public Guid Id { get; private set; }

        /// <summary>Họ và tên.</summary>
        public string Name { get; private set; }

        /// <summary>Email (đã chuẩn hoá chữ thường).</summary>
        public string Email { get; private set; }

        /// <summary>Mã số sinh viên.</summary>
        public string StudentCode { get; private set; }

        /// <summary>Thời điểm tham gia hệ thống (UTC).</summary>
        public DateTime JoinedAt { get; private set; }

        /// <summary>Trạng thái hoạt động.</summary>
        public MemberStatus Status { get; private set; }

        private Member(string name, string email, string studentCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            StudentCode = studentCode;
            JoinedAt = DateTime.UtcNow;
            Status = MemberStatus.Active;
        }

        /// <summary>Tạo thành viên mới, kiểm tra dữ liệu đầu vào.</summary>
        /// <exception cref="ArgumentException">Tên, email hoặc MSSV không hợp lệ.</exception>
        public static Member Create(string name, string email, string studentCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên không được để trống.", nameof(name));
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Email không hợp lệ.", nameof(email));
            if (string.IsNullOrWhiteSpace(studentCode))
                throw new ArgumentException("MSSV không được để trống.", nameof(studentCode));

            return new Member(name.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim());
        }

        /// <summary>Thành viên có đang hoạt động không.</summary>
        public bool IsActive() => Status == MemberStatus.Active;

        /// <summary>Ngừng hoạt động thành viên.</summary>
        public void Deactivate() => Status = MemberStatus.Inactive;

        /// <inheritdoc />
        public override string ToString() => $"{Name} ({StudentCode})";
    }
}
