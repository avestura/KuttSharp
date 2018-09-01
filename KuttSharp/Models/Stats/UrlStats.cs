using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class UrlStats
    {
        [JsonProperty("id")]
        [JsonRequired]
        public string Id { get; set; }

        [JsonProperty("total")]
        [JsonRequired]
        public long TotalClicks { get; set; }

        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("lastDay")]
        public StatsSection LastDay { get; set; }

        [JsonProperty("lastWeek")]
        public StatsSection LastWeek { get; set; }

        [JsonProperty("lastMonth")]
        public StatsSection LastMonth { get; set; }

        [JsonProperty("allTime")]
        public StatsSection AllTime { get; set; }
    }
}
