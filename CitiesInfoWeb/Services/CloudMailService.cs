namespace CitiesInfoWeb.Services
{
    public class CloudMailService : IMailService 
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;
        public CloudMailService(IConfiguration configuration) //..с помощью IConfiguration допуск к appsettingsjson
        {
            _mailTo = configuration["MailSettings:mailToAddress"];
            _mailFrom = configuration["MailSettings:mailFromAddress"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo} with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject {subject}, message {message}");
        }
    }
}
