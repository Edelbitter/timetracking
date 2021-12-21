using System.Globalization;
using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Services
{
    public class FileService : IFileService
    {
        private static string timeFilePath = "time";
        private static string stateFilePath = "running";

        public bool CheckRunning()
        {
            return File.Exists(stateFilePath);
        }

        public void StartRecording()
        {
            if (File.Exists(stateFilePath))
            {
                throw new Exception("is already running");
            }

            File.Create(stateFilePath).Close();

            using var fileStream = File.AppendText(timeFilePath);
            fileStream.WriteLine($"start {DateTime.Now.ToString("G",CultureInfo.GetCultureInfo("de-DE"))}");
            fileStream.Close();
        }

        public void StopRecording()
        {
            if (!File.Exists(stateFilePath))
            {
                throw new Exception("is not running");
            }

            File.Delete(stateFilePath);

            using var fileStream = File.AppendText(timeFilePath);
            fileStream.WriteLine($"stop {DateTime.Now.ToString("G", CultureInfo.GetCultureInfo("de-DE"))}");
            fileStream.Close();
        }
    }
}
