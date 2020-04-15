using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public interface IRateService
    {
        List<Rate> Read();
    }
}
