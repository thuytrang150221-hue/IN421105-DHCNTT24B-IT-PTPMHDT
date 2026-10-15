using System;
using System.Collections.Generic;
using System.Text;

namespace EDUHUB_CH01.Domain
{
    /// <summary>Các giá trị hợp lệ của <see cref="Registration.Status"/>.</summary>
    public static class RegistrationStatus
    {
        /// <summary>Mới đăng ký, chờ xác nhận.</summary>
        public const string Pending = "Pending";
        /// <summary>Đã được xác nhận.</summary>
        public const string Confirmed = "Confirmed";
        /// <summary>Đã huỷ (trạng thái cuối).</summary>
        public const string Cancelled = "Cancelled";
    }

    /// <summary>Phiếu đăng ký của một thành viên cho một sự kiện (bảng nối M:N).</summary>
    public class Registration
    {
        /// <summary>Định danh duy nhất.</summary>
        public Guid Id { get; private set; }

        /// <summary>Thành viên đăng ký (khoá ngoại).</summary>
        public Guid MemberId { get; private set; }

        /// <summary>Sự kiện được đăng ký (khoá ngoại).</summary>
        public Guid EventId { get; private set; }

        /// <summary>Thời điểm đăng ký (UTC).</summary>
        public DateTime RegisteredAt { get; private set; }

        /// <summary>Trạng thái, xem <see cref="RegistrationStatus"/>.</summary>
        public string Status { get; private set; }

        private Registration(Guid memberId, Guid eventId)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = DateTime.UtcNow;
            Status = RegistrationStatus.Pending;
        }

        /// <summary>Tạo phiếu đăng ký ở trạng thái Pending.</summary>
        public static Registration Create(Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty) throw new ArgumentException("Thiếu MemberId.", nameof(memberId));
            if (eventId == Guid.Empty) throw new ArgumentException("Thiếu EventId.", nameof(eventId));

            return new Registration(memberId, eventId);
        }

        /// <summary>Xác nhận phiếu. Chỉ hợp lệ khi đang Pending.</summary>
        /// <exception cref="InvalidOperationException">Phiếu không ở trạng thái Pending.</exception>
        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
                throw new InvalidOperationException($"Không thể xác nhận phiếu đang ở trạng thái {Status}.");
            Status = RegistrationStatus.Confirmed;
        }

        /// <summary>Huỷ phiếu. Hợp lệ khi Pending hoặc Confirmed.</summary>
        /// <exception cref="InvalidOperationException">Phiếu đã huỷ trước đó.</exception>
        public void Cancel()
        {
            if (Status == RegistrationStatus.Cancelled)
                throw new InvalidOperationException("Phiếu đã được huỷ trước đó.");
            Status = RegistrationStatus.Cancelled;
        }
    }
}
