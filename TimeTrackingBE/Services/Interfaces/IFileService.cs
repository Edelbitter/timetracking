namespace TimeTrackingBE.Services.Interfaces
{
    public interface IFileService
    {
        void StartRecording();
        void StopRecording();
        bool CheckRunning();
        Task<List<string>> GetCurrentWeekLines();
    }
}
