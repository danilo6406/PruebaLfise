using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;


namespace EasySales.Server.Models.Repositories
{
    internal class EmailSenderRepository : IEmailSender
    {
        private readonly IParametrosSistemaRepository parametrosSistemaRepository;

        public EmailSenderRepository(IParametrosSistemaRepository parametrosSistemaRepository)
        {
            this.parametrosSistemaRepository = parametrosSistemaRepository;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("EasySales", "info@serviciosintegras.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("Nuevo Usuario", email);
            message.To.Add(to);

            message.Subject = subject;

            ////mensaje HTML personalizado.
            //BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = (@"<b>NUEVA SOLICITUD DE PROVEEDORES</b><br/>
            //        <br/>  
            //        <p>Nombre de la persona: <b>Danilo</b> <br/> " +
            //        "Numero identificación: <b>0012222323dzdx</b> <br/> " +
            //        "Correo electrónico: <b>" + email + "</b> <br/> " +
            //        "# Teléfono: <b>225566333</b> <br/> " +
            //        "Saludos Cordiales</p>");

            //Adjuntar archivos
            //bodyBuilder.Attachments.Add(env.WebRootPath + "\\file.png");
            //message.Body = bodyBuilder.ToMessageBody();

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            SmtpClient smtp = new SmtpClient();
            //client.CheckCertificateRevocation = false;
            string ContraseñaSMTP = await parametrosSistemaRepository.ObtenerValorStringXNombre("ContraseñaSMTP");
            string EmailSMTP = await parametrosSistemaRepository.ObtenerValorStringXNombre("EmailSMTP"); 
            string ServidorSMTP = await parametrosSistemaRepository.ObtenerValorStringXNombre("ServidorSMTP");
            int PuertoSMTP = Convert.ToInt32(await parametrosSistemaRepository.ObtenerValorNumericoXNombre("PuertoSMTPGmail"));

            smtp.Connect(ServidorSMTP, PuertoSMTP, SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailSMTP, ContraseñaSMTP);

            var response = await smtp.SendAsync(message);
            smtp.Disconnect(true);
            smtp.Dispose();

        }
    }
}
