using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class Language : Content
    {
        public string Code { get; set; }
        public string Name { get; set; }
        [JsonProperty("fallback_locale", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FallbackCode { get; set; }
    }
}
