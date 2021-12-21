using Microsoft.Extensions.Logging;

namespace Enrollment.Bsl.Flow
{
    public class CustomActions : ICustomActions
    {
        private readonly ILogger<CustomActions> logger;

        public CustomActions(ILogger<CustomActions> logger)
        {
            this.logger = logger;
        }

        public void WriteToLog(string message) => this.logger.LogInformation(message);
    }
}
