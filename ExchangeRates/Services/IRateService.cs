using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public interface IRateService
    {
        // reads all Rates
        List<Rate> Read();
        // reads rates based on country-code
        Rate ReadByCountry(string country);
        // decimal-typed conversion rate calculator function
        decimal GetConversionRate(string amount, string fromCountryRate, string toCountryRate);
        // creates a new rate
        Rate CreateRate(Rate newRate);
        // updates an existing rate based on country in search bar and id in json Rate-typed updateRate
        Rate UpdateRate(string country, Rate updateRate);
    }
}
