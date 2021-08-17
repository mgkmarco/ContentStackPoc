using ContentManagementService.Interfaces;
using ContentManagementService.Models;
using ContentStackPoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ContentStackPoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentManagementApiService _contentManagementApiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController([NotNull] IContentManagementApiService contentManagementApiService, ILogger<HomeController> logger)
        {
            _contentManagementApiService = contentManagementApiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public async Task<JsonResult> GetLanguages()
        {
            var locales = await _contentManagementApiService.GetLanguages();

            return Json(locales);
        }

        [HttpGet]
        public async Task<JsonResult> GetAssets()
        {
            var assets = await _contentManagementApiService.GetAssets();

            return Json(assets);
        }
        [HttpGet]
        public async Task<JsonResult> GetAsset([FromQuery] string uid)
        {
            var asset = await _contentManagementApiService.GetAsset(uid);

            return Json(asset);
        }

        [HttpPost]
        public async Task<JsonResult> UploadAsset(AssetDto assetDto)
        {
            var asset = await _contentManagementApiService.UploadAsset(assetDto.Title, assetDto.Type, assetDto.Base64Encoded);

            return Json(asset);
        }

        [HttpPost]
        public async Task<JsonResult> CreateEntry(EntryDto entryDto)
        {
            var entry = new Entry
            {
                Uid = entryDto.EntryUid,
                Locale = entryDto.Locale,
                EntryTitle = entryDto.EntryTitle,
                LinkTitle = entryDto.LinkTitle,
                LinkUrl = entryDto.LinkUrl,
                Assets = entryDto.Assets,
                RichText = entryDto.RichText
            };

            //this is a pretend 
            var siteStructureId = "123";
            var uid = await _contentManagementApiService.UpsertEntry(siteStructureId, entry);

            return Json(uid);
        }

        public IActionResult Entry()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
