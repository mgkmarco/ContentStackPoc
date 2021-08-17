namespace ContentStackPoc.Models
{
    public class EntryDto
    { 
        public string EntryUid { get; set; }
        public string Locale { get; set; }
        public string EntryTitle { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string[] Assets { get; set; }
        public string RichText { get; set; }
    }
}
