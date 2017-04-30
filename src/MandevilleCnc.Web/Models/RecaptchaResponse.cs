using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandevilleCnc.Web.Models
{
    public class RecaptchaResponse
    {
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public ICollection<string> ErrorCodes { get; set; }

        public RecaptchaResponse()
        {
            ErrorCodes = new HashSet<string>();
        }
    }
}
