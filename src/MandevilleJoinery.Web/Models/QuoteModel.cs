using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MandevilleJoinery.Web.Helpers;

namespace MandevilleJoinery.Web.Models
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
                var greeting =
                    DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12 ? "Good morning"
                    : DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 5 ? "Good afternoon"
                    : "Good evening";

                var message = string.Format("{0} Team Mandeville", greeting);                
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("{0} has requested a quote through mandevillejoinery.com.", Name);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "=====================================================================================";
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Email: {0}", Email);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Telephone: {0}", Telephone);

                if (BestContactTime != null)
                {
                    message += Environment.NewLine;
                    message += Environment.NewLine;
                    message += string.Format("Best contact time: {0}", BestContactTime);
                }

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

                return message;
            }
        }
    }
}
