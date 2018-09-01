using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    internal class KuttError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
