using ContentManagementService.Interfaces;
using ContentManagementService.Models;
using ContentStackPoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ContentStackPoc.Controllers
{
    public class EntryController : Controller
    {
        private readonly IContentManagementApiService _contentManagementApiService;
        private readonly ILogger<EntryController> _logger;

        public EntryController([NotNull] IContentManagementApiService contentManagementApiService, ILogger<EntryController> logger)
        {
            _contentManagementApiService = contentManagementApiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetEntry([FromQuery] string uid, [FromQuery] string locale, [FromQuery] int version)
        {
            var entry = await _contentManagementApiService.GetEntry(uid, locale, version);

            return Json(entry);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllEntries()
        {
            var entries = await _contentManagementApiService.GetEntries();

            return Json(entries);
        }

        [HttpPost]
        public async Task<JsonResult> UpsertEntry(EntryDto entryDto)
        {
            var entry = new Entry
            {
                Uid = entryDto.EntryUid,
                Locale = entryDto.Locale,
                EntryTitle = entryDto.EntryTitle,
                LinkTitle = entryDto.LinkTitle,
                LinkUrl = entryDto.LinkUrl,
                Assets = entryDto.Assets
            };

            //this is a pretend 
            var siteStructureId = "123";
            var uid = await _contentManagementApiService.UpsertEntry(siteStructureId, entry);

            return Json(uid);
        }

        [HttpPost]
        public async Task<ActionResult> PublishEntry(PublishDto publishDto)
        {
            await _contentManagementApiService.PublishEntry(publishDto.Uid, publishDto.Locale);

            return Json("");
        }
    }
}
