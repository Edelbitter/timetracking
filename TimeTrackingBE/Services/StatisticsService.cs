using Common.Models;

using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IFileService fileService;

        public StatisticsService(IFileService fs)
        {
            fileService = fs;
        }

        public async Task<WeekStats> GetCurrentWeekAsync()
        {
            var weekLines = await fileService.GetCurrentWeekLines();
            var weekTimes = new Dictionary<DateTime, string>();
            foreach (var line in weekLines)
            {
                var splits = line.Split(" ");
                var time = DateTime.Parse(splits[1]);
                weekTimes.Add(time,splits[0]);
            }

            var weekTimesGrouped = weekTimes.GroupBy(wt => wt.Key.Day,
                (day)=>new
                {
                    Key=day,
                    Value=
                });
            
        }
    }
}
