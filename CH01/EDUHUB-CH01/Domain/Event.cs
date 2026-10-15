using System;
using System.Collections.Generic;
using System.Text;

namespace EDUHUB_CH01.Domain
{
    /// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
    public class Event
    {
        /// <summary>Định danh duy nhất.</summary>
        public Guid Id { get; private set; }

        /// <summary>Câu lạc bộ tổ chức (khoá ngoại).</summary>
        public Guid ClubId { get; private set; }

        /// <summary>Tiêu đề sự kiện.</summary>
        public string Title { get; private set; }

        /// <summary>Ngày giờ diễn ra.</summary>
        public DateTime Date { get; private set; }

        /// <summary>Sức chứa tối đa.</summary>
        public int Capacity { get; private set; }

        /// <summary>Sự kiện trực tuyến hay tại chỗ.</summary>
        public bool IsOnline { get; private set; }

        private Event(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
        {
            Id = Guid.NewGuid();
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
            IsOnline = isOnline;
        }

        /// <summary>Tạo sự kiện mới.</summary>
        /// <exception cref="ArgumentException">Tiêu đề trống hoặc sức chứa không dương.</exception>
        public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
        {
            if (clubId == Guid.Empty)
                throw new ArgumentException("Sự kiện phải thuộc một CLB.", nameof(clubId));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề không được để trống.", nameof(title));
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa phải lớn hơn 0.");

            return new Event(clubId, title.Trim(), date, capacity, isOnline);
        }

        /// <summary>Còn chỗ không, với số đã đăng ký cho trước.</summary>
        public bool HasCapacity(int registeredCount) => registeredCount < Capacity;
    }
}
