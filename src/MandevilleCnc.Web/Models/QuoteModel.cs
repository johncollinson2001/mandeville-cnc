using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MandevilleCnc.Web.Models
{
    public class QuoteModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string BestContactTime { get; set; }

        [Required]
        public string ProjectDetails { get; set; }

        public string Message {
            get
            {
                var message = string.Format("{0} has requested a quote through mandevillecnc.com.", Name);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "The clients details are:";
                message += Environment.NewLine;
                message += string.Format("Email: {0}", Email);
                message += Environment.NewLine;
                message += string.Format("Telephome: {0}", Telephone);
                message += Environment.NewLine;
                message += string.Format("Best contact time: {0}", BestContactTime);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += "Project Details:";
                message += Environment.NewLine;
                message += ProjectDetails;

                return message;
            }
        }
    }
}
