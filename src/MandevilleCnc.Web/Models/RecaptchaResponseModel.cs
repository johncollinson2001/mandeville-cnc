using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandevilleCnc.Web.Models
{
    public class RecaptchaResponseModel
    {
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public ICollection<string> ErrorCodes { get; set; }

        public RecaptchaResponseModel()
        {
            ErrorCodes = new HashSet<string>();
        }
    }
}
