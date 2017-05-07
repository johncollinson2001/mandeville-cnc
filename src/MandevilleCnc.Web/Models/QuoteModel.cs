using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MandevilleCnc.Web.Helpers;

namespace MandevilleCnc.Web.Models
{
    public class QuoteModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9()\-+ ]+$", ErrorMessage = "Please enter a valid phone number.")]
        public string Telephone { get; set; }

        public string BestContactTime { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please type at least 10 characters in your project details.")]
        [Display(Name = "Project Details")]
        public string ProjectDetails { get; set; }

        /// <summary>
        /// Formats the quote into a message that can be emailed.
        /// </summary>
        public string EmailMessage {
            get
            {
                var message = string.Format("{0} has requested a quote through mandevillecnc.com.", Name);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "The clients details are:";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Email: {0}", Email);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Telephome: {0}", Telephone);

                if (BestContactTime != null)
                {
                message += Environment.NewLine;
                    message += Environment.NewLine;
                    message += string.Format("Best contact time: {0}", BestContactTime);
                }

                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "Project Details:";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += ProjectDetails;
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
