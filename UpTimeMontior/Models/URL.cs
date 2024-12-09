using Microsoft.Identity.Client;

namespace UpTimeMontior.Models
{
    public class URL
    {
        public int Id { get; set; }
        public string FullUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? UpTime { get; set; }
        public int? AverageRTT { get; set; }

    }
}
