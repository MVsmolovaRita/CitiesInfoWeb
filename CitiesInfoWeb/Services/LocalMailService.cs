namespace CitiesInfoWeb.Services
{
    public class LocalMailService: IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;
        public LocalMailService(IConfiguration configuration) //..с помощью IConfiguration допуск к appsettingsjson
        {
            _mailTo = configuration["MailSettings:mailToAddress"];
            _mailFrom = configuration["MailSettings:mailFromAddress"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo} with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject {subject}, message {message}");
        }
    }
}
