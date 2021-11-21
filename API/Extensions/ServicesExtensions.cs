using API.Services.BotServices;
using API.Services.BotServices.MessageService;
using API.Services.InstructorService;
using API.Services.TimerService;
using Application.CloudStorage;
using Application.MappingProfiles;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var botConfiguration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .WithOrigins("http://localhost:3000");
                });
            });

            services.AddHostedService<ConfigurationService>();

            services.AddHttpClient("tgwebhook").AddTypedClient<ITelegramBotClient>(client => new TelegramBotClient(botConfiguration.Token, client));

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IInstructorService, InstructorService>();

            services.AddSingleton<ITimerService, TimerService>();
            services.AddHostedService(provider => provider.GetService<ITimerService>());

            services.AddScoped<ICloudStorage, CloudStorage>();

            services.AddControllers().AddNewtonsoftJson();

            return services;
        }
    }
}