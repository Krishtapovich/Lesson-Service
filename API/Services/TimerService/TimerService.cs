using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories.SurveyRepository;
using Microsoft.Extensions.DependencyInjection;

namespace API.Services.TimerService
{
    public class TimerService : ITimerService
    {
        private Dictionary<Guid, Timer> timers;
        private readonly ISurveyRepository repository;

        public TimerService(IServiceProvider provider)
        {
            repository = provider.CreateScope().ServiceProvider.GetRequiredService<ISurveyRepository>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timers = new Dictionary<Guid, Timer>();
            return Task.CompletedTask;
        }

        public Task AddTimerAsync(Guid surveyId, int period)
        {
            timers.Add(surveyId, new Timer(DoWork, surveyId, TimeSpan.FromSeconds(period), TimeSpan.Zero));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var surveyId = (Guid)state;
            await repository.ChangeSurveyStatusAsync(surveyId, false);
            await timers[surveyId].DisposeAsync();
            timers.Remove(surveyId);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timers.Clear();
            return Task.CompletedTask;
        }
    }
}
