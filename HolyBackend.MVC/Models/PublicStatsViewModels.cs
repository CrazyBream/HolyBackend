using System;
using System.Collections.Generic;

namespace HolyBackend.MVC.Models
{
    public class OrderStatisticsViewModel
    {
        public string SelectedPeriod { get; set; } = "all";
        public string PeriodLabel { get; set; } = "За весь час існування Ордену";

        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int InProgressOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CancelledOrders { get; set; }

        public Dictionary<string, int> OrdersByType { get; set; } = new();

        public int SuccessRatePercentage => TotalOrders > 0
            ? (int)Math.Round((double)CompletedOrders / TotalOrders * 100)
            : 0;
    }

    public class HolySponsorViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string CustomTitle { get; set; } = string.Empty;
        public int CompletedOrdersCount { get; set; }
        public int TotalOrdersCount { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsManualSponsor { get; set; }
    }
}