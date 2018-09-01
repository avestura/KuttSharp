using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class StatsSection
    {
        [JsonProperty("stats")]
        public ClientStats ClientStats { get; set; }

        [JsonProperty("views")]
        public List<long> ViewStats { get; set; }
    }
}
