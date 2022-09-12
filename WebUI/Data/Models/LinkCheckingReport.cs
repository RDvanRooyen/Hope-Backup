using System;

namespace WebUI.Data.Models
{
    public partial class LinkCheckingReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string ExternalLink { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public DateTime? NotificationSentDate { get; set; }

    }
}
