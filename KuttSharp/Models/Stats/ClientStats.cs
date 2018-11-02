using KuttSharp.Models.Stats;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KuttSharp.Models
{
    public class ClientStats
    {
        /// <summary>
        /// Indicates with what browsers clients visited your link
        /// </summary>
        [JsonProperty("browser")]
        public List<NameValueStats> Browser { get; set; }

        /// <summary>
        /// Indicates with what operating systems clients visited your link
        /// </summary>
        [JsonProperty("os")]
        public List<NameValueStats> OperatingSystem { get; set; }

        /// <summary>
        /// Indicates from what countries users visited your link
        /// </summary>
        [JsonProperty("country")]
        public List<NameValueStats> Country { get; set; }

        /// <summary>
        /// Indicates from what urls users refered to your link
        /// </summary>
        [JsonProperty("referrer")]
        public List<NameValueStats> Referrer { get; set; }
    }
}