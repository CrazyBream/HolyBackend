using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyBackend.Core.Patterns.Strategy
{
    public interface IClientRankStrategy
    {
        bool IsApplicable(int completedOrdersCount);
        string GetTitle();
    }
}