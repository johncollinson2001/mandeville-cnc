﻿using MandevilleJoinery.Web.Models;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using SendGrid;

namespace MandevilleJoinery.Web.Helpers
{
    public static class EmailHelpers
    {
        /// <summary>
        /// States if the given captcha reponse is valid.
        /// </summary>
        /// <param name="secret">The recaptcha secret, which you need to get from Google.</param>
        /// <param name="captchaResponse">The captcha response, posted from the form, which needs to be verified.</param>
        /// <param name="host">The ip of the user who is posting the form.</param>
        /// <returns>True if the captcha is valid, false if not.</returns>
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

        /// <summary>
        /// Sends an email using send grid.
        /// </summary>
        /// <param name="to">The address the mail is being sent to.</param>
        /// <param name="from">The address the mail is being sent from.</param>
        /// <param name="name">The name of the sender.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The email message.</param>
        /// <returns>The asynchronous task.</returns>
        public static async Task SendMail(string to, string from, string name,  string subject, string message, string apiKeyEnvironmentVariable)
        {
            var emailMessage = new SendGridMessage();
            emailMessage.SetFrom(new EmailAddress(from, name));
            emailMessage.AddTo(new EmailAddress(to));
            emailMessage.SetSubject(subject);
            emailMessage.AddContent(MimeType.Text, message);

            var apiKey = Environment.GetEnvironmentVariable(apiKeyEnvironmentVariable);
            var client = new SendGridClient(apiKey);
            await client.SendEmailAsync(emailMessage);
        }

        /// <summary>
        /// Gets a random motivational quote, used to add to the bottom of emails if you choose!
        /// </summary>
        /// <returns>A motivational quote that really motivates.</returns>
        public static string GetMotivationalQuote()
        {
            var quotes = new string[] 
            {
                "Tough times never last, but tough people do.",
                "When you want to succeed as bad as you want to breathe, then you’ll be successful.",
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
                "Things work out best for those who make the best of how things work out.",
                "Believe in yourself! Have faith in your abilities! Without a humble but reasonable confidence in your own powers you cannot be successful or happy.",
                "If you can dream it, you can do it.",
                "Where there is a will, there is a way. If there is a chance in a million that you can do something, anything, to keep what you want from ending, do it. Pry the door open or, if need be, wedge your foot in that door and keep it open.",
                "Do not wait; the time will never be ‘just right.’ Start where you stand, and work with whatever tools you may have at your command, and better tools will be found as you go along.",
                "Press forward. Do not stop, do not linger in your journey, but strive for the mark set before you.",
                "The future belongs to those who believe in the beauty of their dreams.",
                "Don’t watch the clock; do what it does. Keep going.",
                "There will be obstacles. There will be doubters. There will be mistakes. But with hard work, there are no limits.",
                "Keep your eyes on the stars, and your feet on the ground.",
                "We aim above the mark to hit the mark.",
                "One way to keep momentum going is to have constantly greater goals.",
                "Change your life today. Don’t gamble on the future, act now, without delay.",
                "You just can’t beat the person who never gives up.",
                "Start where you are. Use what you have. Do what you can.",
                "Why should you continue going after your dreams? Because seeing the look on the faces of the people who said you couldn’t… will be priceless.",
                "Never give up, for that is just the place and time that the tide will turn."
            };

            return quotes[new Random().Next(0, quotes.Length - 1)];
        }
    }
}
