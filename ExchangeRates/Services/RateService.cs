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
