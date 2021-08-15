using Newtonsoft.Json;

namespace ContentStackPoc.Internal.Contracts.Models
{
    public class ImageFieldUpsertRequest
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
