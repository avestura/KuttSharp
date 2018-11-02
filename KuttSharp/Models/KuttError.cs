using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    internal class KuttError
    {
        /// <summary>
        /// Error returned from server
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
