using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp.Models
{
    public class KuttUrlList
    {
        [JsonRequired]
        [JsonProperty("list")]
        public List<KuttUrl> Urls { get; set; }

        [JsonProperty("countAll")]
        public long CountAll { get; set; }
    }
}
