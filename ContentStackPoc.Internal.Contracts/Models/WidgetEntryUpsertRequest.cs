using ContentManagementService.Models;
using Newtonsoft.Json;

namespace ContentManagementService.Interfaces
{
    public class WidgetEntryUpsertRequest
    {
        [JsonProperty("entry")]
        public WidgetContentTypeUpsertRequest Entry { get; set; }
    }
}
