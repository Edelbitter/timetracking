using Common.Models;

namespace TimeTrackingBE.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<WeekStats> GetCurrentWeekAsync();
    }
}
