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

        
    }
}
