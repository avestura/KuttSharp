using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class UrlStats
    {
        /// <summary>
        /// Id of the shortened url
        /// </summary>
        [JsonProperty("id")]
        [JsonRequired]
        public string Id { get; set; }

        /// <summary>
        /// Total clicks on the shortened link
        /// </summary>
        [JsonProperty("total")]
        [JsonRequired]
        public long TotalClicks { get; set; }

        /// <summary>
        /// Shortened version of the original url
        /// </summary>
        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// The original long version of the shortened url
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        /// Stats of the last day
        /// </summary>
        [JsonProperty("lastDay")]
        public StatsSection LastDay { get; set; }

        /// <summary>
        /// Stats of the last week
        /// </summary>
        [JsonProperty("lastWeek")]
        public StatsSection LastWeek { get; set; }

        /// <summary>
        /// Stats of the last month
        /// </summary>
        [JsonProperty("lastMonth")]
        public StatsSection LastMonth { get; set; }

        /// <summary>
        /// Stats for all time
        /// </summary>
        [JsonProperty("allTime")]
        public StatsSection AllTime { get; set; }
    }
}
