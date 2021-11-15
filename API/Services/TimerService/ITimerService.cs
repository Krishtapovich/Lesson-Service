using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace API.Services.TimerService
{
    public interface ITimerService : IHostedService
    {
        Task AddTimerAsync(Guid surveyId, int period);
    }
}
