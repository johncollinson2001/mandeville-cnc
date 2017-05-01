using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MandevilleCnc.Web.Models;
using MandevilleCnc.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace MandevilleCnc.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: / 
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /pagenotfound
        /// </summary>
        [HttpGet]
        public IActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// GET: /error
        /// </summary>
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// GET: /aboutus
        /// </summary>
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        /// <summary>
        /// GET: /contact
        /// </summary>
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// GET: /getaquote
        /// </summary>
        [HttpGet]
        public IActionResult GetAQuote()
        {
            return View();
        }

        /// <summary>
        /// POST: /getaquote
        /// </summary>
        [HttpPost]
        public IActionResult GetAQuote(QuoteModel model, [Bind(Prefix = "g-recaptcha-response")] string recaptchaResponse)
        {
            return ValidateAndSendMessage(model.Name, model.Email, model.Message, recaptchaResponse);
        }

        /// <summary>
        /// POST: /sendmessage
        /// </summary>
        [HttpPost]
        public IActionResult SendMessage(MessageModel model, [Bind(Prefix = "g-recaptcha-response")] string recaptchaResponse)
        {
            return ValidateAndSendMessage(model.Name, model.Email, model.Message, recaptchaResponse);
        }

        /// <summary>
        /// Validates and sends a message (e.g. a message or a quote)
        /// </summary>
        private IActionResult ValidateAndSendMessage(string name, string email, string message, string recaptchaResponse)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            // Validate recaptcha
            if (!EmailHelpers.IsRecaptchaValid("6LfGYx8UAAAAAI-3Y8bzfGSBroFVfaCcSIdW77g1", recaptchaResponse, Request.Host.Host).Result)
            {
                return new BadRequestResult();
            }

            // Send quote request
            EmailHelpers.SendMail("info@mandevillejoinery.com", email, name,
                "Message sent through mandevillecnc.com", message, "smtp.mandevillejoinery.com").Wait();

            return new OkResult();
        }
    }
}
