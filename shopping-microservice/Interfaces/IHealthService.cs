using System.Threading.Tasks;

namespace shopping_microservice.Interfaces
{
    public class HealthResult
    {
        public string Uptime { get; set; }
        public string Status { get; set; }
    }

    public interface IHealthService
    {
        Task<HealthResult> TryCheckDatabaseHealthAsync();
    }
}