using System;

namespace HolyBackend.Core.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Client";

        public string CustomTitle { get; set; } = "Новачок";
        public int CompletedOrdersCount { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public bool IsBanned { get; set; } = false;
        public string? BanReason { get; set; }
        public DateTime? BannedUntil { get; set; }

        public bool IsHolySponsor { get; set; } = false;
    }
}