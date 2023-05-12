using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TT.Interfaces;
using TT.Models;
using TT.Providers;
using TT.Services;

namespace TT
{
    static class Program
    {
        /// <summary>
        /// Главный метод программы, который читает настройки приложения из файла конфигурации appsettings.json,
        /// создает необходимые сервисы и запускает отправку электронной почты через EmailService.
        /// </summary>
        /// <param name="args"> Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IAppSettings>(appSettings)
                .AddSingleton<IAppSettingsProvider, AppSettingsProvider>()
                .AddTransient<EmailService>()
                .BuildServiceProvider();

            var emailService = serviceProvider.GetService<EmailService>();

            emailService.SendEmail("Тема письма", "Текст письма");
        }
    }
}
