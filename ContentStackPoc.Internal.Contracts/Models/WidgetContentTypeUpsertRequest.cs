using ContentStackPoc.Internal.Contracts.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentManagementService.Models
{
    public class WidgetContentTypeUpsertRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("texts")]
        public List<TextField> Texts { get; set; }

        [JsonProperty("links")]
        public List<LinkField> Links { get; set; }

        [JsonProperty("images")]
        public List<ImageFieldUpsertRequest> Images { get; set; }
    }
}
