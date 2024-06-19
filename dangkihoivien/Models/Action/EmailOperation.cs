using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace dangkihoivien.Models.Action
{
    public class EmailOperation
    {
        public void SendEmail(string emailTo, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                var emailAdminAddress = new MailboxAddress("Admin", "cuog2805@gmail.com");
                var emailHoiVienAddress = new MailboxAddress("Hội viên", emailTo);

                email.From.Add(emailAdminAddress);
                email.To.Add(emailHoiVienAddress);

                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = body
                };
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("cuog2805@gmail.com", "oimr dbqa fnmw xjjc");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}