using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MandevilleCnc.Web.Models;
using MandevilleCnc.Web.Helpers;

namespace MandevilleCnc.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAQuote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAQuote(QuoteModel model, [Bind(Prefix = "g-recaptcha-response")] string recaptchaResponse)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            // Validate recaptcha
            if (!EmailHelpers.IsRecaptchaValid("6LfGYx8UAAAAAI-3Y8bzfGSBroFVfaCcSIdW77g1", recaptchaResponse, Request.Host.Host).Result)
            {
                return Json(new
                {
                    status = false,
                    errors = new List<string>() { "Please answer the recaptcha challenge." }
                });
            }

            // Send quote request
            try
            {
                EmailHelpers.SendMail("info@mandevillejoinery.com", model.Email, model.Name, 
                    "Message sent through mandevillecnc.com", model.Message, "smtp.mandevillejoinery.com").RunSynchronously();

                return Json(new { status = true });
            }
            catch
            {
                // should log ex here

                return Json(new
                {
                    status = false,
                    errors = new List<string>() { "Could not send message." }
                });
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
