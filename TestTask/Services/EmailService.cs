using TT.Interfaces;

namespace TT.Services;

/// <summary>
/// Предоставляет сервис электронной почты, который использует настройки,
/// определенные интерфейсом IAppSettings для отправки сообщений электронной почты.
/// </summary>
public class EmailService
{
    private readonly IAppSettings _appSettings;
    ///<summary>
    /// Инициализирует новый экземпляр класса EmailService с заданными настройками.
    ///</summary>
    ///<param name="appSettings">
    /// Объект, реализующий интерфейс IAppSettings,
    /// который предоставляет настройки для отправки сообщений электронной почты.
    /// </param>
    public EmailService(IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    ///<summary>
    /// Отправляет сообщение на электронную почту
    /// с указанной темой и текстом на адрес, указанный в настройках.
    ///</summary>
    ///<param name="subject"> Тема сообщения электронной почты.</param>
    ///<param name="body"> Текст сообщения электронной почты.</param>
    public void SendEmail(string subject, string body)
    {
        // здесь отправка электронной почты
        Console.WriteLine($"Отправлено письмо на адрес {_appSettings.EmailToAddress}");
    }
}