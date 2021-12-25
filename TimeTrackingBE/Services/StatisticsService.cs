using Common.Models;

using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IFileService fileService;
        private static readonly TimeSpan targetHoursPerDay = TimeSpan.FromHours(8);
        private static readonly List<TimeSpan> dailyTargets = new List<TimeSpan>()
        {
            targetHoursPerDay,
            targetHoursPerDay*2,
            targetHoursPerDay*3,
            targetHoursPerDay*4,
            targetHoursPerDay*5,
        };

        public StatisticsService(IFileService fs)
        {
            fileService = fs;
        }

        public async Task<WeekStats> GetCurrentWeekAsync()
        {
            var weekLines = await fileService.GetCurrentWeekLines();
            var weekTimes = new List<(DateTime time, string action)>();
            foreach (var line in weekLines)
            {
                var splits = line.Split(" ");
                var time = DateTime.Parse(splits[1]);
                weekTimes.Add((time,splits[0]));
            }

            var weekTimesGrouped = weekTimes.GroupBy(wt => wt.time.Day,
                wt => wt).OrderBy(wt=>wt.Key);

            var hoursPerDay = new List<TimeSpan>();
            foreach (var dayTimeStamps in weekTimesGrouped)
            {
                TimeSpan workedtime = SumTime(dayTimeStamps);
                hoursPerDay.Add(workedtime);
            }

            while (hoursPerDay.Count() < 5)
            {
                hoursPerDay.Add(TimeSpan.Zero);
            }

            var result = new WeekStats();
            result.DonePerDay = hoursPerDay;
            result.SumDailyTargetUpToDay = dailyTargets;
            result.SumDonePerDayUpToDay = new List<TimeSpan>()
            {
                hoursPerDay.First(),
                hoursPerDay.Take(2).Aggregate((a, b) => a + b),
                hoursPerDay.Take(3).Aggregate((a, b) => a + b),
                hoursPerDay.Take(4).Aggregate((a, b) => a + b),
                hoursPerDay.Take(5).Aggregate((a, b) => a + b),
            };

            result.CurrentTotalDone = result.SumDonePerDayUpToDay.Last();
            result.ToGoPerWeek = dailyTargets.Last() - result.CurrentTotalDone;

            return result;
        }

        private TimeSpan SumTime(IGrouping<int, (DateTime time, string action)> dayTimeStamps)
        {
            TimeSpan result = TimeSpan.Zero;

            // handle unfinished day
            var dayTimeStampsL = dayTimeStamps.OrderBy(dt => dt.time).AsEnumerable().ToList();
            if (dayTimeStampsL.Last().action == "start")
            {
                dayTimeStampsL.Add((DateTime.Now, "stop"));
            }

            for(int i = 0; i < dayTimeStampsL.Count();i+=2)
            {
                if (dayTimeStampsL[i].action == "start" && dayTimeStampsL[i + 1].action == "stop")
                {
                    var diff = dayTimeStampsL[i + 1].time.Subtract(dayTimeStampsL[i].time);
                    result += diff;
                }
                else
                {
                    Console.WriteLine("Error in times file");
                }
            }

            return result;
        }
    }
}
