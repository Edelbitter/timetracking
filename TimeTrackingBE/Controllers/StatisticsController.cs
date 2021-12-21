using Microsoft.AspNetCore.Mvc;
using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService ss)
        {
            statisticsService = ss;
        }


    }
}
