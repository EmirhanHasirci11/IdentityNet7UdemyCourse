using IdentityNet7UdemyLesson.web.OptionsModel;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityNet7UdemyLesson.web.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _options;

        public EmailService(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetEmailLink, string ToEmail)
        {
            var smptClient = new SmtpClient();
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials=new NetworkCredential(_options.Email,_options.Password);
            smptClient.EnableSsl = true;
            smptClient.Host = _options.Host;
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_options.Email);
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = "LocalHost | Şifre sıfırlama Linki";
            mailMessage.Body = $@"
                    <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız</h4>
                    <p><a href='{resetEmailLink}'>Şifre yenileme linki</a></p>";
            mailMessage.IsBodyHtml = true;
            await smptClient.SendMailAsync(mailMessage);
        }
    }
}
