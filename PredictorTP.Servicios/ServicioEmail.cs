using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace PredictorTP.Servicios
{

    public interface IServicioEmail
    {
        Task EnviarConfirmacion(string destinatario, string asunto, string msj);
    }
    public class ServicioEmail : IServicioEmail
    {
        public async Task EnviarConfirmacion(string destinatario, string asunto, string msjHtml)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Predictor", "predictortppw3@gmail.com"));
            message.To.Add(new MailboxAddress("", destinatario));
            message.Subject = asunto;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = msjHtml
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("predictortppw3@gmail.com", "ihef olrv vsuc woaq");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
