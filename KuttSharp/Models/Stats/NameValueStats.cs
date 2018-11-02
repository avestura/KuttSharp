using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models.Stats
{
    public class NameValueStats
    {
        /// <summary>
        /// Name of the stats item
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Value of the stats item
        /// </summary>
        [JsonProperty("value")]
        public long Value { get; set; }
    }
}
