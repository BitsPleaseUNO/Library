using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Library.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            string text = string.Format("Please click on this link to {0}: {1}", subject, message);
            string html = "Please confirm your account by clicking this link:" + message + "<br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message);

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("library@quickdev.top");
            msg.To.Add(new MailAddress(email));
            msg.Subject = subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("jetstreamsgo.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("library@quickdev.top", "booksrdumb");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
            return Task.CompletedTask;
        }
    }
}
