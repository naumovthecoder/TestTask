namespace TT.Interfaces;

/// <summary>
///  Определяет свойства для хранения настроек подключения к БД и SMTP-серверу,
/// а также для указания адресов отправителя и получателя электронной почты.
/// </summary>
public interface IAppSettingsProvider
{
    /// <summary>
    ///  Определяет метод, который получает экземпляр интерфейса IAppSettings,
    /// содержащий значения настроек приложения, необходимых для его работы.
    /// </summary>
    /// <returns>Экземпляр интерфейса IAppSettings с настройками приложения.
    /// </returns>
    IAppSettings GetAppSettings();
}