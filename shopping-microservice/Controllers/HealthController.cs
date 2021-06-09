using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shopping_microservice.Interfaces;

namespace shopping_microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger, IHealthService healthService)
        {
            _logger = logger;
            _healthService = healthService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var healthResult = await _healthService.TryCheckDatabaseHealthAsync().ConfigureAwait(false);
            if (healthResult.Status == "Healthy") return Ok(healthResult);
            return NoContent();
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var process = Process.GetCurrentProcess();
            var stats = new
            {
                os = new
                {
                    description = RuntimeInformation.OSDescription,
                    type = Enum.GetName(RuntimeInformation.OSArchitecture),
                    bit64 = Environment.Is64BitOperatingSystem
                },
                framework = RuntimeInformation.FrameworkDescription,
                containerized = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") is object
                    ? "true"
                    : "false",
                user = new
                {
                    username = Environment.UserName
                },
                machine = new
                {
                    name = Environment.MachineName
                },
                cpu = new
                {
                    cores = Environment.ProcessorCount
                },
                memory = new
                {
                    currentUsage = $"{ConvertBytesToMegabytes(process.WorkingSet64)} MB",
                    maxAvailable = $"{ConvertBytesToMegabytes(process.MaxWorkingSet.ToInt64())} MB"
                }
            };

            return Ok(stats);
        }

        private static double ConvertBytesToMegabytes(long bytes)
        {
            return bytes / 1024f / 1024f;
        }
    }
}