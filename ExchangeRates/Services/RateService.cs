using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public Rate CreateRate(Rate newRate)
        {
            return _rateRepository.CreateRate(newRate);
        }

        public decimal GetConversionRate(string amount, string fromCountryRate, string toCountryRate)
        {
            decimal decimalNumber;
            if (decimal.TryParse(amount, out decimalNumber))
            {
                if (decimalNumber > 0)
                {          
                    Rate validatedFcr = _rateRepository.ReadByCountry(fromCountryRate);
                    if (validatedFcr != null)
                    {
                        Rate validatedTcr = _rateRepository.ReadByCountry(toCountryRate);
                        if (validatedTcr != null)
                        {
                            decimal conversionRateFcr = decimal.Parse(validatedFcr.Rate1.ToString());
                            decimal conversionRateTcr = decimal.Parse(validatedTcr.Rate1.ToString());
                            decimal exchangeRate = conversionRateFcr / conversionRateTcr;
                            decimal conversionValue = 1 / exchangeRate;
                            decimal result = decimalNumber * conversionValue;
                        return result;
                        }
                    }
                }
            }
            return 0;
        }

        public List<Rate> Read()
        {
            return _rateRepository.Read();
        }

        public Rate ReadByCountry(string country)
        {
            return _rateRepository.ReadByCountry(country);
        }

        public Rate UpdateRate(string country, Rate updateRate)
        {
            var validatedRate =_rateRepository.ReadByCountry(country);
            if (validatedRate != null)
            {
                return _rateRepository.UpdateRate(updateRate);
            } else
            {
                Console.WriteLine("Could not find given rate.");
                return updateRate;
            }

        }
    }
}
