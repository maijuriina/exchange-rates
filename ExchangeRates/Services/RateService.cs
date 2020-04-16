using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class RateService : IRateService
    {
        // inject repository layer
        private readonly IRateRepository _rateRepository;

        // constructor for injected repository layer
        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        // calls CreateRate through repository rate
        public Rate CreateRate(Rate newRate)
        {
            return _rateRepository.CreateRate(newRate);
        }

        // calculates the conversion rate based on user input strings
        public decimal GetConversionRate(string amount, string fromCountryRate, string toCountryRate)
        {
            decimal decimalNumber;
            if (decimal.TryParse(amount, out decimalNumber)) // parse decimal
            {
                if (decimalNumber > 0) // check if decimal number is over 0 - ACCEPTS 0,1 etc. IN SEARCH BAR!
                {          
                    Rate validatedFcr = _rateRepository.ReadByCountry(fromCountryRate); // fetches the Rate object according to the country code
                    if (validatedFcr != null) // checks for nulls, if null, returns 0 to controller
                    {
                        Rate validatedTcr = _rateRepository.ReadByCountry(toCountryRate); // same here
                        if (validatedTcr != null) // same here
                        {
                            decimal conversionRateFcr = decimal.Parse(validatedFcr.Rate1.ToString()); // makes Rate object's decimal calculable
                            decimal conversionRateTcr = decimal.Parse(validatedTcr.Rate1.ToString()); // same here
                            decimal exchangeRate = conversionRateFcr / conversionRateTcr; // divides conversion rates with each other and saved to parameter
                            decimal conversionValue = 1 / exchangeRate; // conversionValue saved from dividing one by the exchange rate
                            decimal result = decimalNumber * conversionValue; // result is amount of currency multiplied by its conversionValue 
                        return result; // amount of currency for desired amount in other currency
                        }
                    }
                }
            }
            return 0; // 0 is bound in controller to show up an error message
        }

        // repository is called with Read, returning a List of Rates
        public List<Rate> Read()
        {
            return _rateRepository.Read();
        }

        // the user's country code is given to repository function readbycountry and returns its value Rate
        public Rate ReadByCountry(string country)
        {
            return _rateRepository.ReadByCountry(country);
        }

        // updates desired Rate by country tag, jsonobject Rate including id
        public Rate UpdateRate(string country, Rate updateRate)
        {
            var validatedRate =_rateRepository.ReadByCountry(country); // save repository's function result to a var value
            if (validatedRate != null) // if does not return null, carry on with UpdateRate through repository
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
