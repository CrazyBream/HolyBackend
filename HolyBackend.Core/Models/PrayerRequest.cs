using System.ComponentModel.DataAnnotations;

namespace HolyBackend.Core.Models
{
    public class PrayerRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Сповідь має бути підписана")]
        public string DeveloperName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Розкажіть про свого демона")]
        public string BugDescription { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsExorcised { get; set; } = false;
    }
}