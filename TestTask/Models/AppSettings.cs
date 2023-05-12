using TT.Interfaces;

namespace TT.Models;

/// <summary>
/// Представляет набор свойств, описывающих настройки приложения,
/// которые могут быть загружены из конфигурационного файла.
/// </summary>
public class AppSettings : IAppSettings
{
    public string ConnectionString { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string EmailFromAddress { get; set; }
    public string EmailToAddress { get; set; }
}
