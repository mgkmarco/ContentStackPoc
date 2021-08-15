using Newtonsoft.Json;
using System;

namespace ContentStackPoc.Internal.Contracts.Models
{
    public class PublishDetail
    {
        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
