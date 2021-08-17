using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class Entry : Content
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("entryTitle")]
        public string EntryTitle { get; set; }

        [JsonProperty("linktitle")]
        public string LinkTitle { get; set; }

        [JsonProperty("linkurl")]
        public string LinkUrl { get; set; }

        [JsonProperty("assets")]
        public string[] Assets { get; set; }

        [JsonProperty("richtext")]
        public string RichText { get; set; }
    }
}
