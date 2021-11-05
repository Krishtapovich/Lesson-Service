using API.Services.BotServices;
using Domain.Repositories.BotRepository;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace API.Extensions
{
    public static class BotServicesExtensions
    {
        public static IServiceCollection AddBotServices(this IServiceCollection services, BotConfiguration botConfiguration)
        {
            services.AddHostedService<BotConfigurationService>();

            services.AddHttpClient("tgwebhook")
                .AddTypedClient<ITelegramBotClient>(client => new TelegramBotClient(botConfiguration.Token, client));

            services.AddSingleton<IBotRepository, BotRepository>();
            services.AddSingleton<BotUpdateService>();

            return services;
        }
    }
}