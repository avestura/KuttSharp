using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class KuttUrl
    {
        /// <summary>
        /// Unique ID of the URL
        /// </summary>
        [JsonProperty("id")]
        [JsonRequired]
        public string Id { get; set; }

        /// <summary>
        /// DateTime of when the URL was created
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Where the URL will redirect to
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        /// Whether or not a password is required
        /// </summary>
        [JsonProperty("password")]
        public bool IsPasswordRequired { get; set; }

        /// <summary>
        /// The amount of visits to this URL
        /// </summary>
        [JsonProperty("count")]
        public long Count { get; set; }

        /// <summary>
        /// The shortened link (Usually https://kutt.it/id)
        /// </summary>
        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }
    }
}
