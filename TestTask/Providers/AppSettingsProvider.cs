using Microsoft.Extensions.DependencyInjection;
using TT.Interfaces;

namespace TT.Providers;

/// <summary>
/// Предоставляет реализацию интерфейса для получения настроек приложения.
/// </summary>
public class AppSettingsProvider : IAppSettingsProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса AppSettingsProvider с заданным провайдером сервисов.
    /// </summary>
    /// <param name="serviceProvider">
    /// Провайдер сервисов, который будет использоваться для получения зависимости.
    /// </param>
    public AppSettingsProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Возвращает объект, реализующий интерфейс настроек приложения,
    /// используя зависимость, зарегистрированную в контейнере сервисов.
    /// </summary>
    /// <returns>
    /// Объект, реализующий интерфейс настроек приложения.
    /// </returns>
    public IAppSettings GetAppSettings()
    {
        return _serviceProvider.GetRequiredService<IAppSettings>();
    }
}