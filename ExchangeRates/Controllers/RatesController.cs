using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRates.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        // inject service layer
        private readonly IRateService _rateService;

        // constructor for service with null-check
        public RatesController(IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
        }

        // returns all data in table
        // GET: api/Rates
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_rateService.Read());
        }

        // returns data according to country search
        // GET: api/Rates/USD
        [HttpGet("{country}", Name = "Get")]
        public IActionResult Get(string country)
        {
            if (_rateService.ReadByCountry(country) != null)
            {
                return new JsonResult(_rateService.ReadByCountry(country));
            } else
            {
                return new JsonResult("Your search terms found no matches from the database.");
            }

        }

        // returns either invalid search terms OR the conversion rate based on user input
        // GET: api/Rates/amount/fromCountryRate/toCountryRate
       [HttpGet("{amount}/{fromCountryRate}/{toCountryRate}")]
        public IActionResult Get(string amount, string fromCountryRate, string toCountryRate)
        {
            if (_rateService.GetConversionRate(amount, fromCountryRate, toCountryRate) == 0)
            {
                return new JsonResult("Invalid search terms. Query unsuccessful.");
            } else
            {
                return new JsonResult(_rateService.GetConversionRate(amount, fromCountryRate, toCountryRate));
            }
            
        }

        // creates a new Rate-type currency
        // POST: api/Rates
        [HttpPost]
        public IActionResult Post(Rate newRate)
        {
            return new JsonResult(_rateService.CreateRate(newRate));
        }

        // updates a currency's information based on what is put in updateRate : NOTE! Requires id in json
        // PUT: api/Rates/USD
        [HttpPut("{country}")]
        public IActionResult Put(string country, Rate updateRate)
        {
            return new JsonResult(_rateService.UpdateRate(country, updateRate));
        }
    }
}
