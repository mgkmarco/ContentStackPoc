using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class LinkField
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("link")]
        public List Link { get; set; }
    }

    public class List
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
