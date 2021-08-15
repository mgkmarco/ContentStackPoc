using Newtonsoft.Json;
using System;

namespace ContentManagementService.Models
{
    public class Content
    {
        public string Uid { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("created_by", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("updated_by", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty("_version", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Version { get; set; }
    }
}
