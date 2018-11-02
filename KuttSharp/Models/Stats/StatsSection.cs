using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class StatsSection
    {
        /// <summary>
        /// Stats related to client (e.g. browser, OS, country...)
        /// </summary>
        [JsonProperty("stats")]
        public ClientStats ClientStats { get; set; }

        /// <summary>
        /// Number of views for the past hours, days or months based on the stats context
        /// </summary>
        [JsonProperty("views")]
        public List<long> ViewStats { get; set; }
    }
}
