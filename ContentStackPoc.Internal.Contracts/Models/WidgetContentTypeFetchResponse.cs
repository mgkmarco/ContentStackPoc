using ContentStackPoc.Internal.Contracts.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentManagementService.Models
{
    public class WidgetContentTypeFetchResponse : Content
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("texts")]
        public List<TextField> Texts { get; set; }

        [JsonProperty("links")]
        public List<LinkField> Links { get; set; }

        [JsonProperty("images")]
        public List<ImageFieldAssetFetchResponse> Images { get; set; }
        
        [JsonProperty("publish_details", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<PublishDetail> PublishDetails { get; set; }


    }
}
