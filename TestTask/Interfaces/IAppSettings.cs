namespace TT.Interfaces;

/// <summary>
/// Интерфейс для получения настроек приложения.
/// </summary>
public interface IAppSettings
{
    string ConnectionString { get; }
    string SmtpServer { get; }
    int SmtpPort { get; }
    string SmtpUsername { get; }
    string SmtpPassword { get; }
    string EmailFromAddress { get; }
    string EmailToAddress { get; }
}