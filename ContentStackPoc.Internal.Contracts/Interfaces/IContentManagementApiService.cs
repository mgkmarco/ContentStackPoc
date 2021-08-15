using ContentManagementService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagementService.Interfaces
{
    public interface IContentManagementApiService
    {
        Task<IEnumerable<WidgetContentTypeFetchResponse>> GetEntries(string contentType = "widget_content", bool includePublishDetails = true);
        Task<IEnumerable<Language>> GetLanguages();
        Task<IEnumerable<Asset>> GetAssets();
        Task<Asset> GetAsset(string uid);
        Task<Asset> UploadAsset(string title, string assetType, string base64Encoded);
        Task<WidgetContentTypeFetchResponse> GetEntry(string entryUid, string locale, int version = 0, string contentType = "widget_content");
        Task<bool> EntryExists(string entryUid, string contentType = "widget_content");
        Task<string> UpsertEntry(string siteStructureId, Entry entry, string contentType = "widget_content");
        Task PublishEntry(string uid, string locale, string contentType = "widget_content");
    }
}