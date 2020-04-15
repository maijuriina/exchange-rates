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

        public List<Rate> Read()
        {
            return _exchangeRatesDbContext.Rate.ToList();
        }
    }
}
