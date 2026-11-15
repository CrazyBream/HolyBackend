using System;

namespace HolyBackend.MVC.Models
{
    public class ClientCrmViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string RankTitle { get; set; } = string.Empty;
        public DateTime FirstContactDate { get; set; }
        public int TotalOrders { get; set; }
        public int InProgressOrders { get; set; }
        public int CompletedOrders { get; set; }
        public bool IsBanned { get; set; }
        public string? BanReason { get; set; }
        public DateTime? BannedUntil { get; set; }
        public bool IsHolySponsor { get; set; }
    }
}