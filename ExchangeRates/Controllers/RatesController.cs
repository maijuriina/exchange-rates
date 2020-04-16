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
            return new JsonResult(_rateService.ReadByCountry(country));
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

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
