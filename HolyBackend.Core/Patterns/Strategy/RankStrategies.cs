using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Strategy
{
    public class NewbieRankStrategy : IClientRankStrategy
    {
        public bool IsApplicable(int completedOrdersCount) => completedOrdersCount < 2;
        public string GetTitle() => "Новачок";
    }

    public class RegularClientRankStrategy : IClientRankStrategy
    {
        public bool IsApplicable(int completedOrdersCount) => completedOrdersCount >= 2 && completedOrdersCount < 5;
        public string GetTitle() => "Постійний парафіянин";
    }

    public class MasterRankStrategy : IClientRankStrategy
    {
        public bool IsApplicable(int completedOrdersCount) => completedOrdersCount >= 5;
        public string GetTitle() => "Магістр Ордену";
    }
}