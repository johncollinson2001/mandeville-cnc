using MailKit.Net.Smtp;
using MailKit.Security;
using MandevilleCnc.Web.Models;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MandevilleCnc.Web.Helpers
{
    public static class EmailHelpers
    {
        public static async Task<bool> IsRecaptchaValid(string secret, string captchaResponse, string host)
        {
            var requestUrl = string.Format(
                "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}",
                secret,
                captchaResponse,
                host);

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(requestUrl);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    var obj = JsonConvert.DeserializeObject<RecaptchaResponse>(result);
                    return obj.Success;
                }
            }

            return false;
        }

        public static async Task SendMail(string to, string from,string name,  string subject, string message, string smtp)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(name, from));
            emailMessage.To.Add(new MailboxAddress(string.Empty, to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtp, 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
