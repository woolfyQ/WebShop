using MimeKit;
using MailKit.Net.Smtp;
using Core.Entity;

namespace Application.Services
{
    public class EmailService
    {
        public async Task SendEmail(UserMessage userMessage)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администратор", "volkov.v.s.ru@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("Adm", "volkov.v.s.ru@yandex.ru"));
            emailMessage.ReplyTo.Add(new MailboxAddress(userMessage.Name, userMessage.Email));
            emailMessage.Subject = "Новое сообщение с сайта";
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Сообщение от:\nИмя: {userMessage.Name}\nEmail: {userMessage.Email}\n\n{userMessage.Message}"
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync("volkov.v.s.ru@yandex.ru", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

            }
        }


    }
}
