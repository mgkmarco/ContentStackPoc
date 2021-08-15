using Newtonsoft.Json;

namespace ContentManagementService.Models
{
    public class ImageFieldAssetFetchResponse
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("image")]
        public ImageAssetFetchResponse Image { get; set; }
    }

    public class ImageAssetFetchResponse : Content
    {       
        [JsonProperty("content_type", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("file_size", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FileSize { get; set; }

        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Filename { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }
    }
}
