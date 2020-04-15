using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IRateRepository
    {
        List<Rate> Read();
    }
}
