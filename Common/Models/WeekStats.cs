using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class WeekStats
    {
        public List<TimeSpan> DonePerDay { get; set; }
        public List<TimeSpan> SumDonePerDayUpToDay { get; set; }
        public List<TimeSpan> SumDailyTargetUpToDay { get; set; }
        public TimeSpan CurrentTotalDone { get; set; }
        public TimeSpan ToGoPerWeek { get; set; }
    }
}
