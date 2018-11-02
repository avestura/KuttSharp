using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class KuttUrlList
    {
        /// <summary>
        /// List of Url objects
        /// </summary>
        [JsonRequired]
        [JsonProperty("list")]
        public List<KuttUrl> Urls { get; set; }

        /// <summary>
        /// Amount of items in the list
        /// </summary>
        [JsonProperty("countAll")]
        public long CountAll { get; set; }
    }
}
