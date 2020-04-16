using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public class RateRepository : IRateRepository
    {
        // inject database for repository's use
        private readonly ExchangeRatesDbContext _exchangeRatesDbContext;

        public RateRepository(ExchangeRatesDbContext exchangeRatesDbContext)
        {
            _exchangeRatesDbContext = exchangeRatesDbContext ?? throw new ArgumentNullException(nameof(exchangeRatesDbContext));
        }

        public Rate CreateRate(Rate newRate)
        {
            _exchangeRatesDbContext.Rate.Add(newRate);
            _exchangeRatesDbContext.SaveChanges();
            return newRate;
        }

        public List<Rate> Read()
        {
            return _exchangeRatesDbContext.Rate.ToList();
        }

        public Rate ReadByCountry(string country)
        {
            return _exchangeRatesDbContext.Rate.AsNoTracking()
                .FirstOrDefault(r => r.Country == country);
        }
    }
}
