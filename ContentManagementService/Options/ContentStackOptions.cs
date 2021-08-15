namespace ContentManagementService.Options
{
    public class ContentStackOptions
    {
        public static string SectionKey = "ContentStack";
        public string ManagementUrl { get; set; }
        public TokenOptions Tokens { get; set; }
    }
}
