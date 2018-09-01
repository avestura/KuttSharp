using KuttSharp.Models.Stats;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KuttSharp.Models
{
    public class ClientStats
    {
        [JsonProperty("browser")]
        public List<NameValueStats> Browser { get; set; }

        [JsonProperty("os")]
        public List<NameValueStats> OperatingSystem { get; set; }

        [JsonProperty("country")]
        public List<NameValueStats> Country { get; set; }

        [JsonProperty("referrer")]
        public List<NameValueStats> Referrer { get; set; }

    }
}