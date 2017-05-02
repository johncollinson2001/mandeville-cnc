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
                    var obj = JsonConvert.DeserializeObject<RecaptchaResponseModel>(result);
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

        public static string GetMotivationalQuote()
        {
            var quotes = new string[] 
            {
                "Tough times never last, but tough people do.",
                "When you want to succeed as bad as you want to breathe,21 then you’ll be successful.",
                "Hard work beats talent when talent doesn’t work hard.",
                "A man can be as great as he wants to be. If you believe in yourself and have the courage, the determination, the dedication, the competitive drive and if you are willing to sacrifice the little things in life and pay the price for the things that are worthwhile, it can be done.",
                "Every great story on the planet happened when someone decided not to give up, but kept going no matter what.",
                "Don’t stop when you’re tired. STOP when you are DONE.",
                "Failure will never overtake me if my determination to succeed is strong enough.",
                "Success… seems to be connected with action. Successful people keep moving. They make mistakes, but they don’t quit.",
                "Aim for the moon. If you miss, you may hit a star.",
                "Action is the foundational key to all success.",
                "The ones who are crazy enough to think they can change the world, are the ones who do.",
                "Successful people do what unsuccessful people are not willing to do. Don’t wish it were easier, wish you were better.",
                "The question isn’t who is going to let me; it’s who is going to stop me.",
                "It does not matter how slowly you go, so long as you do not stop.",
                "Little minds are tamed and subdued by misfortune; but great minds rise above it.",
                "I find that the harder I work, the more luck I seem to have.",
                "Twenty years from now you will be more disappointed by the things that you didn’t do than by the ones you did do, so throw off the bowlines, sail away from safe harbor, catch the trade winds in your sails. Explore, Dream, Discover.",
                "You don’t have to be great to start, but you have to start to be great.",
                "Nothing is impossible, the word itself says “I’m possible”!",
                "Life begins at the end of your comfort zone.",
                "The will to win, the desire to succeed, the urge to reach your full potential… these are the keys that will unlock the door to personal excellence.",
                "Things work out best for those who make the best of how things work out."
            };

            return quotes[new Random().Next(0, quotes.Length - 1)];
        }
    }
}
