using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class Asset : Content
    {
        [JsonProperty("content_type", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContetyType { get; set; }
        [JsonProperty("file_size", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FileSize { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }
    }
}
