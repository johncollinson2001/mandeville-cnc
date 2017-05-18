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
                var message = string.Format("{0} has sent a message through mandevillejoinery.com.", Name);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "The clients details are:";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Email: {0}", Email);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "Message:";
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
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "Have a great day!";

                return message;
            }
        }
    }
}
