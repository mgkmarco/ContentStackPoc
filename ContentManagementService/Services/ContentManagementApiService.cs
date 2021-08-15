using ContentManagementService.Interfaces;
using ContentManagementService.Models;
using ContentStackPoc.Internal.Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagementService.Services
{
    public class ContentManagementApiService : IContentManagementApiService
    {
        private readonly IContentManagementClient _contentManagementClient;

        public ContentManagementApiService([NotNull] IContentManagementClient contentManagementClient)
        {
            _contentManagementClient = contentManagementClient;
        }

        public async Task<WidgetContentTypeFetchResponse> GetEntry(string entryUid, string locale, int version = 0, string contentType = "widget_content")
        {
            var queryParams = new QueryParams();

            if(!string.IsNullOrEmpty(locale))
            {
                queryParams.Locale = locale;
            }

            if(version > 0)
            {
                queryParams.Version = version;
            }

            var response = await _contentManagementClient.GetEntry(contentType, entryUid, queryParams);
            var widgetContentType = (WidgetContentTypeFetchResponse)response["entry"].ToObject(typeof(WidgetContentTypeFetchResponse));

            return widgetContentType;
        }

        public async Task<bool> EntryExists(string entryUid, string contentType = "widget_content")
        {
            var response = await _contentManagementClient.GetEntry(contentType, entryUid);
            
            return response != null;
        }

        public async Task<IEnumerable<Language>> GetLanguages()
        {
            var response = await _contentManagementClient.GetLanguages();
            var languages = response["locales"].Select(s => (Language)s.ToObject(typeof(Language)));

            return languages;
        }

        public async Task<IEnumerable<WidgetContentTypeFetchResponse>> GetEntries(string contentType = "widget_content", bool includePublishDetails = true)
        {
            var response = await _contentManagementClient.GetEntries(contentTypeUid: contentType, includePublishDetails: true.ToString().ToLower());
            var entries = response["entries"].Select(s => (WidgetContentTypeFetchResponse)s.ToObject(typeof(WidgetContentTypeFetchResponse)));

            return entries;
        }

        public async Task<Asset> GetAsset(string uid)
        {
            var response = await _contentManagementClient.GetAsset(uid);
            var asset = (Asset)response["asset"].ToObject(typeof(Asset));

            return asset;
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            var response = await _contentManagementClient.GetAssets();
            var assets = response["assets"].Select(s => (Asset)s.ToObject(typeof(Asset)));

            return assets;
        }

        public async Task<Asset> UploadAsset(string title, string assetType, string base64Encoded)
        {
            var byteArr = Convert.FromBase64String(base64Encoded);
            var response = await _contentManagementClient.PostAsset(new ByteArrayPart(byteArr, title, assetType));
            var asset = (Asset)response["asset"].ToObject(typeof(Asset));
            
            return asset;
        }

        public async Task<string> UpsertEntry(string siteStructureId, Entry entry, string contentType = "widget_content")
        {
            var images = new List<ImageFieldUpsertRequest>();

            var exists = false;
            JObject response = null;

            if (!string.IsNullOrEmpty(entry.Uid))
            {
                exists = await EntryExists(entry.Uid);
            }

            if (exists)
            {
                var links = PopulateLinks(entry.EntryTitle, entry.LinkTitle, entry.LinkUrl);

                for (int i = 0; i < entry.Assets.Length; i++)
                {
                    images.Add(new ImageFieldUpsertRequest { Key = $"{entry.EntryTitle}_{nameof(WidgetContentTypeFetchResponse.Images)}_key{i + 1}", Image = entry.Assets[i] });
                }

                var widgetContentType = new WidgetContentTypeUpsertRequest
                {
                    Title = entry.EntryTitle,
                    Texts = new List<TextField>
                {
                    new TextField
                    {
                        Key = $"{entry.EntryTitle}_{nameof(WidgetContentTypeFetchResponse.Texts)}_key{1}",
                        Text = entry.EntryTitle
                    }
                },
                    Links = links,
                    Images = images
                };

                response = await _contentManagementClient.UpdateEntry(contentType, entry.Uid, entry.Locale, new WidgetEntryUpsertRequest { Entry = widgetContentType });
            }
            else
            {
                var entryKey = $"{siteStructureId}_{entry.EntryTitle}";
                var links = PopulateLinks(entryKey, entry.LinkTitle, entry.LinkUrl);

                for (int i = 0; i < entry.Assets.Length; i++)
                {
                    images.Add(new ImageFieldUpsertRequest { Key = $"{entryKey}_{nameof(WidgetContentTypeFetchResponse.Images)}_key{i + 1}", Image = entry.Assets[i] });
                }

                var widgetContentType = new WidgetContentTypeUpsertRequest
                {
                    Title = entryKey,
                    Texts = new List<TextField>
                {
                    new TextField
                    {
                        Key = $"{entryKey}_{nameof(WidgetContentTypeFetchResponse.Texts)}_key{1}",
                        Text = entry.EntryTitle
                    }
                },
                    Links = links,
                    Images = images
                };

                response = await _contentManagementClient.PostEntry(contentType, entry.Locale, new WidgetEntryUpsertRequest { Entry = widgetContentType });
            }

            var uid = response["entry"]["uid"].ToString();

            return uid;
        }

        public async Task PublishEntry(string uid, string locale, string contentType = "widget_content")
        {
            var publishEntry = new PublishEntry
            {
                Entries = new List<EntryReduced>
                {
                    new EntryReduced
                    {
                        ContentType = contentType,
                        Uid = uid
                    }
                },
                Environments = new List<string> { "bltfbd609e336cf2044" },
                Locales = new List<string> { locale }
            };

            var publishResponse = await _contentManagementClient.PublishEntry(publishEntry);
        }

        private List<LinkField> PopulateLinks(string entryKey, string title, string url)
        {
            var links = new List<LinkField>();

            var link = new LinkField
            {
                Key = $"{entryKey}_{nameof(WidgetContentTypeFetchResponse.Links)}_key1",
                Link = new List { Title = title, Href = url }
            };

            links.Add(link);

            return links;
        }
    }
}
