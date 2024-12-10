using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RapidReadr.Server.Models
{
    public class ActivelyReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string path { get; set; }
        public DateTime dateUploaded { get; set; }
        [Required]
        public int timestamp { get; set; }
    }
}
