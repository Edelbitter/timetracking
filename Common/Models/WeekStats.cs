using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class WeekStats
    {
        public List<TimeSpan> Done { get; set; }
        public List<TimeSpan> DaySum { get; set; }
        public List<TimeSpan> DayTarget { get; set; }
        public TimeSpan CurrentSaldo { get; set; }
        public TimeSpan ToGo { get; set; }
    }
}
