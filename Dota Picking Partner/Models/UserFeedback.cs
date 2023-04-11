namespace Dota_Picking_Partner.Models
{
    public class UserFeedback
    {
        public string? Comment { get; set; }
        public string? ContactInfo { get; set; }

    }

    public class RecentFeedback
    {
        public List<Dictionary<string, object>> recentFeedback { get; set; }
    }
}
