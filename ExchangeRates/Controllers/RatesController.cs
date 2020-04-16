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
        private readonly IRateService _rateService;

        public RatesController(IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
        }

        // GET: api/Rates
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_rateService.Read());
        }

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

        // POST: api/Rates
        [HttpPost]
        public IActionResult Post(Rate newRate)
        {
            return new JsonResult(_rateService.CreateRate(newRate));
        }

        // PUT: api/Rates/USD
        [HttpPut("{country}")]
        public IActionResult Put(string country, Rate updateRate)
        {
            return new JsonResult(_rateService.UpdateRate(country, updateRate));
        }
    }
}
