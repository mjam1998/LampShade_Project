using MimeKit;
using MailKit.Net.Smtp;


namespace _0_Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string title, string messageBody, string destination)
        {
            var message = new MimeMessage();
            //مبدا
            var from = new MailboxAddress("Atriya", "test@atriya.com");
            message.From.Add(from);
            //مقصد
            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>{messageBody}</h1>",
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            //ادرس و پورت هاستی که میخریم را وارد میکنیم
            client.Connect("185.88.152.251", 25, false);
            //ادرس اییمیل و رمز ایمیل 
            client.Authenticate("test@atriya.com", "Atriya.123456");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}