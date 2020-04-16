using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public interface IRateService
    {
        List<Rate> Read();
        Rate ReadByCountry(string country);
        decimal GetConversionRate(string amount, string fromCountryRate, string toCountryRate);
        Rate CreateRate(Rate newRate);
        Rate UpdateRate(string country, Rate updateRate);
    }
}
