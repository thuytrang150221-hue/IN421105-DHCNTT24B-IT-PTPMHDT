namespace EduHub.Ch02.Patterns.Strategies;

/// <summary>[ĐÃ HOÀN THÀNH TODO 3 - STRATEGY PATTERN]: Interface chiến lược xuất báo cáo.</summary>
public interface IReportStrategy
{
    string Name { get; }
    string Render(IReadOnlyList<ClubStat> stats);
}
