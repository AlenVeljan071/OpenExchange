using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenExchange.Configuration;
using OpenExchange.Data;
using OpenExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace OpenExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenExchangeController : ControllerBase
    {
        private readonly DbInteractor _context;
        public readonly IOptions<Openexchangerates> _options;
        string ApiKey = "";
        public OpenExchangeController(DbInteractor context, IOptions<Openexchangerates> options)
        {
            _context = context;
            _options = options;
            ApiKey = options.Value.ApiKey;
        }

        [HttpGet("exchange")]
        public async Task<ActionResult<RatesEx>> GetExchange()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://openexchangerates.org/api/");
                    var response = await client.GetAsync($"latest.json?app_id={ApiKey}&symbols=EUR,GBP");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<RootEx>(stringResult);
                    var dbRoot = new RootEx()
                    {
                        RootExId = Guid.NewGuid().ToString(),
                        Disclaimer = deserializeResponse.Disclaimer,
                        License = deserializeResponse.License,
                        Timestamp = deserializeResponse.Timestamp,
                        @base = deserializeResponse.@base,
                        Rates = deserializeResponse.Rates
                    };
                    dbRoot.Rates.RatestId = Guid.NewGuid().ToString();
                    dbRoot.Rates.DateRate = DateTime.UtcNow;

                    await _context.RatesExs.AddAsync(dbRoot.Rates);
                    await _context.RootsExs.AddAsync(dbRoot);
                    await _context.SaveChangesAsync();

                    return dbRoot.Rates;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        [HttpGet("byDate")]
        public async Task<ActionResult<IEnumerable<RootEx>>> GetByDateRate(DateTime fromDate, DateTime toDate)
        {
         
           var dateTo = toDate.AddDays(1);
           return await _context.RootsExs.Include(x => x.Rates).Where(x => x.Rates.DateRate >= fromDate && x.Rates.DateRate <= dateTo).ToListAsync();
        }

        // response sa API - Ne mogu da pristupim endpointu sa free nalogom
        string json = "{\"disclaimer\": \"...\",\"license\": \"...\",\"start_time\": \"2022-03-20T11:00:00Z\",\"end_time\": \"2022-03-27T11:29:59Z\",\"base\": \"USD\",\"rates\": { \"EUR\": {\"open\": 0.872674,\"high\": 0.872674,\"low\": 0.87203, \"close\": 0.872251,  \"average\": 0.872253},   \"GBP\": {    \"open\": 0.765284,\"high\": 0.7657,\"low\": 0.7652,\"close\": 0.765541,\"average\": 0.765503 }}}";

        [HttpGet("period")]
        public async Task<ActionResult<Root>> GetExchangeRate()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://openexchangerates.org/api/");
                    var response = await client.GetAsync($"ohlc.json?app_id={ApiKey}&start_time=2022-03-20T00:00:00Z&period = 1w&base=USD&symbols=GBP,EUR&prettyprint=true");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<Root>(json);

                    var dbRoot = new Root()
                    {
                        RootId = Guid.NewGuid().ToString(),
                        Disclaimer = deserializeResponse.Disclaimer,
                        License = deserializeResponse.License,
                        Start_time = deserializeResponse.Start_time,
                        End_time = deserializeResponse.End_time,
                        @base = deserializeResponse.@base,
                        Rates = deserializeResponse.Rates,
                    };
                    dbRoot.Rates.RatestId = Guid.NewGuid().ToString();
                    dbRoot.Rates.EUR.EurId = Guid.NewGuid().ToString();
                    dbRoot.Rates.GBP.GbpId = Guid.NewGuid().ToString();

                    _context.Rates.Add(dbRoot.Rates);
                    _context.EURs.Add(dbRoot.Rates.EUR);
                    _context.GBPs.Add(dbRoot.Rates.GBP);

                    await _context.Roots.AddAsync(dbRoot);
                    await _context.SaveChangesAsync();

                    return dbRoot;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        [HttpGet("roots")]
        public async Task<ActionResult<IEnumerable<Root>>> GetRates()
        {
            return await _context.Roots.Include(x => x.Rates)
                                        .ThenInclude(x => x.EUR)
                                        .Include(x => x.Rates)
                                        .ThenInclude(x => x.GBP)
                                        .ToListAsync();
        }

        [HttpGet("percent-diference")]
        public async Task<ActionResult<string>> GetExchangeRateSrb()
        {
           
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://kurs.resenje.org/api/v1/");
                    var response = await client.GetAsync("currencies/usd/rates/today");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<BankSrb>(stringResult);

                    var client2 = new HttpClient();
                    client.BaseAddress = new Uri("https://openexchangerates.org/api/");
                    var responseOpenEx = await client.GetAsync($"latest.json?app_id={ApiKey}&symbols=RSD");
                    var stringResultOpenEx = await responseOpenEx.Content.ReadAsStringAsync();
                    var deserializeResponseEx = JsonConvert.DeserializeObject<RootEx>(stringResultOpenEx);
                    double openEx = deserializeResponseEx.Rates.Rsd;
                    double openSr = deserializeResponse.Exchange_middle;
                    var result = Percent(openEx, openSr);
                    return Ok(Math.Round(result, 3) + " %");
                }
                catch
                {
                    return BadRequest();
                }
           
        }
        private double Percent(double x, double y)
        {
            double result = ((x - y) * 100) / x;
            return result;
        }
    }
}


