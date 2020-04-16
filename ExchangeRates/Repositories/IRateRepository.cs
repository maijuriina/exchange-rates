using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IRateRepository
    {
        // read all Rates as List
        List<Rate> Read();
        // read one country by its country code
        Rate ReadByCountry(string country);
        // create new rate and send forward its Rate-object newRate
        Rate CreateRate(Rate newRate);
        // update existing rate and send its Rate-object updateRate
        Rate UpdateRate(Rate updateRate);
    }
}
