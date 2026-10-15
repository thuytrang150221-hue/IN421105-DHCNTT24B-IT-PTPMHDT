# EduHub — Class Diagram

## 1. Domain Classes

```mermaid
classDiagram
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode) Member
        +IsActive() bool
    }

    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +List~Member~ Members
        +Create(name, description) Club
        +AddMember(member) void
        +MemberCount() int
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline) Event
        +HasCapacity(registeredCount) bool
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +Create(memberId, eventId) Registration
        +Confirm() void
        +Cancel() void
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +CheckInMethod Method
        +Create(registrationId, method) CheckIn
        +Summary() string
    }

    class RegistrationStatus {
        <<enumeration>>
        Pending
        Confirmed
        Cancelled
    }

    class CheckInMethod {
        <<enumeration>>
        QRCode
        Manual
    }

    Club "1" o-- "0..*" Member : has members
    Club "1" --> "0..*" Event : organizes
    Member "1" --> "0..*" Registration : registers
    Event "1" --> "0..*" Registration : receives
    Registration "1" --> "0..1" CheckIn : has check-in
    Registration --> RegistrationStatus : status
    CheckIn --> CheckInMethod : method
```

## 2. Relationship Multiplicities

- Một câu lạc bộ có thể có nhiều thành viên; một thành viên có thể tham gia nhiều câu lạc bộ. Trong mô hình này, quan hệ thành viên được thể hiện qua `Club.Members`.
- Một câu lạc bộ có thể tổ chức nhiều sự kiện; mỗi sự kiện thuộc một câu lạc bộ.
- Một thành viên có thể có nhiều phiếu đăng ký; mỗi phiếu đăng ký thuộc một thành viên.
- Một sự kiện có thể có nhiều phiếu đăng ký; mỗi phiếu đăng ký thuộc một sự kiện.
- Một phiếu đăng ký có tối đa một bản ghi check-in; một bản ghi check-in thuộc một phiếu đăng ký.