<h1>Цель: </h1>
Пример идеального кода на C# (.NET), решающий задачу использования конфигурационных файлов с помощью DI контейнера

<h1>Контекст задачи: </h1> 
В проекте используется конфигурационный файл, содержащий настройки для различных сред.  
Каждый раз при запуске приложения нужно загрузить конфигурационный файл для текущей среды и использовать его настройки.  </br></br>
Конфигурационный файл может содержать большое количество параметров, их названия и значения могут меняться от среды к среде. 
Для решения данной задачи можно использовать DI контейнер и конфигурационный файл в формате JSON. 
</br>
</br>
<p>Полный код приложения c комментариями доступен по ссылке: https://github.com/naumovthecoder/TestTask</p>
 
<h1>Решение: </h1>
<h3> 1. Класс настроек конфигурации сервера.</h3>
Класс AppSettings описывает настройки, которые могут быть заданы в конфигурационном файле.</br></br>

```rb
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
```
```rb
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
```


</br><h3>2. Метод GetAppSettings.</h3> 
Возвращает экземпляр объекта, реализующего интерфейс IAppSettings, полученный через IServiceProvider. Это обеспечивает инверсию управления и облегчает управление зависимостями. </br></br>

```rb
public interface IAppSettingsProvider
{
    IAppSettings GetAppSettings();
}
```
```rb
public class AppSettingsProvider : IAppSettingsProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    public AppSettingsProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAppSettings GetAppSettings()
    {
        return _serviceProvider.GetRequiredService<IAppSettings>();
    }
}
```


</br><h3>3. Класс EmailService.</h3> 
Использует интерфейс IAppSettings для отправки электронной почты. В конструкторе он получает экземпляр IAppSettings и сохраняет его в private поле. Метод SendEmail() использует настройки SMTP-сервера для отправки электронной почты. </br></br>

```rb
public class EmailService
{
    private readonly IAppSettings _appSettings;

    public EmailService(IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public void SendEmail(string subject, string body)
    {
        Console.WriteLine($"Отправлено письмо на адрес {_appSettings.EmailToAddress}");
    }
}
```


</br><h3>4. Настройка конфигурации с помощью ConfigurationBuilder.</h3>
В методе Main() происходит настройка конфигурации с помощью ConfigurationBuilder. Затем ConfigurationBuilder используется для создания экземпляра AppSettings и настройки DI-контейнера, который затем используется для получения экземпляра EmailService и вызова его метода SendEmail(). Контейнер DI в данном примере настроен так, что каждый раз, когда он запрашивает экземпляр EmailService, он создает новый экземпляр. </br></br>
 
```java
static class Program
{
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
```
 
 
</br><h1>Пояснение: </h1>
Этот код решает проблему управления зависимостями в приложении. В частности, он использует паттерн Dependency Injection (DI), который позволяет легко изменять зависимости приложения, не изменяя его код. 
</br></br>Благодаря DI, классы не жестко связаны между собой, а зависят от абстракций, которые могут быть реализованы различными способами. Это позволяет легко заменять реализации абстракций, не изменяя код классов, которые от них зависят. 
</br></br>Например, в этом коде интерфейс IAppSettings используется для предоставления доступа к настройкам приложения. Реализация этого интерфейса AppSettings представляет собой простой класс с набором свойств, содержащих значения настроек. Интерфейс IAppSettingsProvider определяет метод, который возвращает объект IAppSettings. Реализация этого интерфейса AppSettingsProvider использует сервис-провайдер для получения экземпляра IAppSettings. 
</br></br>Далее, класс EmailService использует интерфейс IAppSettings для получения настроек электронной почты, и может отправлять письма на адрес, указанный в настройках. 
</br></br>В Main методе создается объект ServiceProvider, который регистрирует все сервисы, используемые приложением, и связывает их между собой. Затем создается объект EmailService с помощью ServiceProvider.GetService<EmailService>(), который получает нужный сервис из контейнера зависимостей и внедряет зависимости в конструктор класса. В конце концов, EmailService.SendEmail метод использует зависимости, чтобы отправить электронное письмо на указанный в настройках адрес. 
 
