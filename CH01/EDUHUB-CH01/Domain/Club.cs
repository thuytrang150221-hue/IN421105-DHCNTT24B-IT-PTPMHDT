using System;
using System.Collections.Generic;
using System.Text;

namespace EDUHUB_CH01.Domain
{
    /// <summary>Câu lạc bộ sinh viên.</summary>
    public class Club
    {
        private readonly List<Member> _members = [];

        /// <summary>Định danh duy nhất.</summary>
        public Guid Id { get; private set; }

        /// <summary>Tên câu lạc bộ.</summary>
        public string Name { get; private set; }

        /// <summary>Mô tả câu lạc bộ.</summary>
        public string Description { get; private set; }

        /// <summary>Thời điểm thành lập (UTC).</summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>Danh sách thành viên (chỉ đọc — muốn thêm phải gọi <see cref="AddMember"/>).</summary>
        public IReadOnlyList<Member> Members => _members;

        private Club(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>Tạo câu lạc bộ mới.</summary>
        /// <exception cref="ArgumentException">Tên trống.</exception>
        public static Club Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên CLB không được để trống.", nameof(name));

            return new Club(name.Trim(), description?.Trim() ?? string.Empty);
        }

        /// <summary>Thêm thành viên; mỗi thành viên chỉ xuất hiện một lần.</summary>
        /// <exception cref="InvalidOperationException">Thành viên đã có trong CLB.</exception>
        public void AddMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member);
            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException($"{member} đã là thành viên của {Name}.");

            _members.Add(member);
        }

        /// <summary>Số thành viên hiện có.</summary>
        public int MemberCount() => _members.Count;
    }
}
