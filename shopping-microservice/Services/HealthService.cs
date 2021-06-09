using System;
using System.Threading.Tasks;
using shopping_microservice.Interfaces;

namespace shopping_microservice.Services
{
    public class HealthService : IHealthService
    {
        public async Task<HealthResult> TryCheckDatabaseHealthAsync()
        {
            // try
            // {
            //     throw new ApplicationException("You didn't whitelist IP address of database. So I can't connect to DB");
            // }
            // catch
            // {
            //     // Ignore the exception 😈
            //     return null;
            // }

            return new HealthResult
            {
                Uptime = "6 hrs",
                Status = "Healthy"
            };
        }
    }
}