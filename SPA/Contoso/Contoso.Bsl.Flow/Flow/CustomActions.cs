using Microsoft.Extensions.Logging;

namespace Contoso.Bsl.Flow
{
    public class CustomActions : ICustomActions
    {
        private readonly ILogger<CustomActions> logger;

        public CustomActions(ILogger<CustomActions> logger)
        {
            this.logger = logger;
        }

        public void WriteToLog(string message) => this.logger.LogInformation(message);

        //public async void WriteToLog(string message)
        //{
        //    await System.Threading.Tasks.Task.Run(() => this.logger.LogInformation(message));
        //}
    }
}
