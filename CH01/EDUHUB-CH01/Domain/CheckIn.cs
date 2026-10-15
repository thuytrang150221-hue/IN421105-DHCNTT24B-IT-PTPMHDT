using System;
using System.Collections.Generic;
using System.Text;

namespace EDUHUB_CH01.Domain
{
    /// <summary>Các hình thức điểm danh hợp lệ của <see cref="CheckIn.Method"/>.</summary>
    public static class CheckInMethod
    {
        /// <summary>Ban tổ chức điểm danh thủ công.</summary>
        public const string Manual = "Manual";
        /// <summary>Quét mã QR.</summary>
        public const string QrCode = "QrCode";
    }

    /// <summary>Ghi nhận sự hiện diện thực tế của một phiếu đăng ký.</summary>
    public class CheckIn
    {
        /// <summary>Định danh duy nhất.</summary>
        public Guid Id { get; private set; }

        /// <summary>Phiếu đăng ký được điểm danh (khoá ngoại, quan hệ 1–1).</summary>
        public Guid RegistrationId { get; private set; }

        /// <summary>Thời điểm điểm danh (UTC).</summary>
        public DateTime CheckedInAt { get; private set; }

        /// <summary>Hình thức điểm danh, xem <see cref="CheckInMethod"/>.</summary>
        public string Method { get; private set; }

        private CheckIn(Guid registrationId, string method)
        {
            Id = Guid.NewGuid();
            RegistrationId = registrationId;
            CheckedInAt = DateTime.UtcNow;
            Method = method;
        }

        /// <summary>Điểm danh cho một phiếu đã được xác nhận.</summary>
        /// <exception cref="InvalidOperationException">Phiếu chưa ở trạng thái Confirmed.</exception>
        public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
        {
            ArgumentNullException.ThrowIfNull(registration);
            if (registration.Status != RegistrationStatus.Confirmed)
                throw new InvalidOperationException("Chỉ phiếu đã Confirmed mới được check-in.");
            if (method is not (CheckInMethod.Manual or CheckInMethod.QrCode))
                throw new ArgumentException($"Hình thức không hợp lệ: {method}.", nameof(method));

            return new CheckIn(registration.Id, method);
        }

        /// <summary>Chuỗi mô tả ngắn để in báo cáo.</summary>
        public string Summary() => $"Check-in {CheckedInAt:HH:mm:ss} qua {Method} (phiếu {RegistrationId.ToString()[..8]})";
    }
}
