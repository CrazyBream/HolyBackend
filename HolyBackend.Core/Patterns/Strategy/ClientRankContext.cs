using System.Collections.Generic;
using System.Linq;

namespace HolyBackend.Core.Patterns.Strategy
{
    public class ClientRankContext
    {
        private readonly IEnumerable<IClientRankStrategy> _strategies;

        public ClientRankContext()
        {
            _strategies = new List<IClientRankStrategy>
            {
                new MasterRankStrategy(),
                new RegularClientRankStrategy(),
                new NewbieRankStrategy()
            };
        }

        public string CalculateTitle(int completedOrders)
        {
            var strategy = _strategies.First(s => s.IsApplicable(completedOrders));
            return strategy.GetTitle();
        }
    }
}