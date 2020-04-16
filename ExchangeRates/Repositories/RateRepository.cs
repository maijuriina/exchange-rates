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

        // constructor for the database context
        public RateRepository(ExchangeRatesDbContext exchangeRatesDbContext)
        {
            _exchangeRatesDbContext = exchangeRatesDbContext ?? throw new ArgumentNullException(nameof(exchangeRatesDbContext));
        }

        // creates a new rate based on values given in newRate
        public Rate CreateRate(Rate newRate)
        {
            _exchangeRatesDbContext.Rate.Add(newRate); // add-function for newRate
            _exchangeRatesDbContext.SaveChanges(); // save changes for database
            return newRate; // return the created rate's info
        }

        // list all Rates
        public List<Rate> Read()
        {
            return _exchangeRatesDbContext.Rate.ToList(); // toList-function for Rate-table through the context
        }

        // read one country by its country code
        public Rate ReadByCountry(string country)
        {
            return _exchangeRatesDbContext.Rate.AsNoTracking() // access the database and rate-table, and set it as something to be only read with AsNoTracking
                .FirstOrDefault(r => r.Country == country); // first value to match the given "country"-code in the table Rate is returned
        }

        // update a currency with info in updateRate
        public Rate UpdateRate(Rate updateRate)
        {
            _exchangeRatesDbContext.Update(updateRate); // update-function for updateRate
            _exchangeRatesDbContext.SaveChanges(); // save changes to database
            return updateRate; // return updated values
        }
    }
}
