using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MandevilleJoinery.Web.Helpers;

namespace MandevilleJoinery.Web.Models
{
    public class MessageModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please type at least 10 characters in your message.")]
        public string Message { get; set; }

        /// <summary>
        /// Formats the message into a message that can be emailed.
        /// </summary>
        public string EmailMessage
        {
            get
            {
                var greeting = 
                    DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12 ? "Good morning"
                    : DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 5 ? "Good afternoon"
                    : "Good evening";

                var message = string.Format("{0} Team Mandeville", greeting);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("{0} has sent a message through mandevillejoinery.com.", Name);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "=====================================================================================";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Message;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "=====================================================================================";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += EmailHelpers.GetMotivationalQuote();

                return message;
            }
        }
    }
}
