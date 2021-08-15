using ContentManagementService.Models;
using Newtonsoft.Json.Linq;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagementService.Interfaces
{
    public interface IContentManagementClient
    {
        [Get("/v3/content_types/{content_type_uid}/entries/{entry_uid}")]
        Task<JObject> GetEntry([AliasAs("content_type_uid")] string contentTypeUid,
            [AliasAs("entry_uid")] string entryUid, QueryParams queryParams = null);

        [Get("/v3/content_types/{content_type_uid}/entries?include_publish_details={include_publish_details}")]
        Task<Dictionary<string, IList<JObject>>> GetEntries([AliasAs("content_type_uid")] string contentTypeUid, [AliasAs("include_publish_details")] string includePublishDetails);

        [Get("/v3/locales")]
        Task<Dictionary<string, IList<JObject>>> GetLanguages();

        [Get("/v3/assets/{uid}")]
        Task<JObject> GetAsset(string uid);

        [Get("/v3/assets")]
        Task<Dictionary<string, IList<JObject>>> GetAssets();

        [Multipart]
        [Post("/v3/assets")]
        Task<JObject> PostAsset([AliasAs("asset[upload]")] ByteArrayPart stream);

        [Post("/v3/content_types/{content_type}/entries?locale={locale}")]
        Task<JObject> PostEntry([AliasAs("content_type")] string contentType, [AliasAs("locale")] string locale, [Body] WidgetEntryUpsertRequest widgetEntry);

        [Put("/v3/content_types/{content_type}/entries/{entry_uid}?locale={locale}")]
        Task<JObject> UpdateEntry([AliasAs("content_type")] string contentType, [AliasAs("entry_uid")] string entryUid, [AliasAs("locale")] string locale, [Body] WidgetEntryUpsertRequest widgetEntry);

        [Post("/v3/bulk/publish?x-bulk-action=publish")]
        Task<JObject> PublishEntry([Body] PublishEntry publishEntry);
    }

    public class QueryParams
    {
        [AliasAs("locale")]
        public string Locale { get; set; } = null;

        [AliasAs("include_publish_details")]
        public string IncludePublishDetails { get; set; } = true.ToString().ToLower();

        [AliasAs("version")]
        public int? Version { get; set; } = null;
    }
}