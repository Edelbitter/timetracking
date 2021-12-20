using Microsoft.AspNetCore.Mvc;
using TimeTrackingBE.Services.Interfaces;

namespace TimeTrackingBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordingController : ControllerBase
    {
        private readonly IFileService fileService;

        public RecordingController(IFileService fs)
        {
            fileService = fs;
        }

        [HttpPost("start")]
        public IActionResult Start()
        {
            try
            {
                fileService.StartRecording();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            try
            {
                fileService.StopRecording();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}