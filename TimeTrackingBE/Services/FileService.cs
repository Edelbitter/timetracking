using System.Globalization;
using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Services
{
    public class FileService : IFileService
    {
        private static string timeFilePath;
        private static string stateFilePath = "running";

        public FileService()
        {
            CultureInfo myCi = new CultureInfo("de-DE");
            Calendar cal = myCi.Calendar;
            timeFilePath = cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
        }

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
            fileStream.WriteLine($"start {DateTime.Now.ToString("s", CultureInfo.GetCultureInfo("de-DE"))}");
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
            fileStream.WriteLine($"stop {DateTime.Now.ToString("s", CultureInfo.GetCultureInfo("de-DE"))}");
            fileStream.Close();
        }

        public async Task<List<string>> GetCurrentWeekLines()
        {
            using var fileStream = File.OpenText(timeFilePath);
            var lines = new List<string>();
            while (!fileStream.EndOfStream)
            {
                var line = await fileStream.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }

            return lines;
        }
    }
}

