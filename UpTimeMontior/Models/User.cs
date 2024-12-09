using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UpTimeMontior.Models
{
    [Index(nameof(UserName), nameof(UserEmail), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string UserName { get; set; } = string.Empty;
        [StringLength(255)]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateLastSignedIn { get; set; }
        public DateTime PasswordExpires { get; set; }
        [JsonIgnore]
        [StringLength(255)]
        public Byte[] PasswordHash { get; set; } = Array.Empty<Byte>();
        [JsonIgnore]
        [StringLength(255)]
        public Byte[] PasswordSalt { get; set; } = Array.Empty<Byte>();
    }
}
