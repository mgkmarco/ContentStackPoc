using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class TextField
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
