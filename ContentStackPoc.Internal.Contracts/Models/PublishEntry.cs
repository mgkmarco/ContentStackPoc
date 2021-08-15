using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentManagementService.Models
{
    public class PublishEntry
    {
        [JsonProperty("entries")]
        public List<EntryReduced> Entries { get; set; }
        [JsonProperty("locales")]
        public List<string> Locales { get; set; }
        [JsonProperty("environments")]
        public List<string> Environments { get; set; }
        [JsonProperty("publish_with_reference")]
        public bool PublishWithReference { get; set; } = true;
        [JsonProperty("skip_workflow_stage_check")]
        public bool SkipWorkflowStageCheck { get; set; } = true;
    }

    public class EntryReduced
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}
