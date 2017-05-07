using Microsoft.AspNetCore.Mvc;
using MandevilleCnc.Web.Models;
using MandevilleCnc.Web.Helpers;
using Microsoft.Extensions.Options;

namespace MandevilleCnc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyOptions _options;

        /// <summary>
        /// Construct.
        /// </summary>
        /// <param name="optionsAccessor">Provides access to the application options.</param>
        public HomeController(IOptions<MyOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

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
            return ValidateAndSendMessage(model.Name, model.Email, model.EmailMessage, recaptchaResponse);
        }

        /// <summary>
        /// POST: /sendmessage
        /// </summary>
        [HttpPost]
        public IActionResult SendMessage(MessageModel model, [Bind(Prefix = "g-recaptcha-response")] string recaptchaResponse)
        {
            return ValidateAndSendMessage(model.Name, model.Email, model.EmailMessage, recaptchaResponse);
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
            if (!EmailHelpers.IsRecaptchaValid(_options.RecaptchaSecret, recaptchaResponse, Request.Host.Host).Result)
            {
                return new BadRequestResult();
            }

            // Send quote request
            var subject = "Message from " + name + ", sent via Mandeville CNC";
            EmailHelpers.SendMail(_options.EmailRecipient, email, name, subject, message, _options.SendGridApiKeyEnvironmentVariableName).Wait();

            return new OkResult();
        }
    }
}
