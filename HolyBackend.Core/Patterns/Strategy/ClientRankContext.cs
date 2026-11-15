using System.Collections.Generic;
using System.Linq;

namespace HolyBackend.Core.Patterns.Strategy
{
    public class ClientRankContext
    {
        private readonly IEnumerable<IClientRankStrategy> _strategies;

        // Підтримує і DI, і створення за замовчуванням
        public ClientRankContext(IEnumerable<IClientRankStrategy>? strategies = null)
        {
            _strategies = strategies != null && strategies.Any()
                ? strategies
                : new List<IClientRankStrategy>
                {
                    new MasterRankStrategy(),
                    new RegularClientRankStrategy(),
                    new NewbieRankStrategy()
                };
        }

        public string CalculateTitle(int completedOrders, bool isHolySponsor = false)
        {
            if (isHolySponsor && completedOrders < 5)
            {
                return "Святий Спонсор";
            }

            var strategy = _strategies.First(s => s.IsApplicable(completedOrders));
            return strategy.GetTitle();
        }
    }
}