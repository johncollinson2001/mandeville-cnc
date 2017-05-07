using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandevilleCnc.Web
{
    public class MyOptions
    {
        public string SendGridApiKeyEnvironmentVariableName { get; set; }
        public string RecaptchaSiteKey { get; set; }
        public string RecaptchaSecret { get; set; }
        public string EmailRecipient { get; set; }
    }
}
