using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockTracker.Data.Adapter.Tiingo;

namespace StockTracker.Data.Service
{
    public class StockDataService : IStockDataService
    {

        private readonly string API_KEY;
        private readonly string URL = "https://api.tiingo.com/";
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;


        private HttpClient client;

        public StockDataService(IConfiguration configuration, ILogger<StockDataService> logger, IWebHostEnvironment environment)
        {
            client = new HttpClient();
            Configuration = configuration;
            API_KEY = Configuration["APIKeys:TiingoAPIKey"];
            _logger = logger;
            _logger.LogInformation(environment.EnvironmentName);
        }

        public TiingoCurrentTopOfBookResponse GetCurrentStockData(string ticker)
        {
            _logger.LogInformation(API_KEY);
            HttpResponseMessage message = client.GetAsync(URL + "/iex/" + ticker + "?token=" + API_KEY).Result;
            message.EnsureSuccessStatusCode();
            string bodyResponse = message.Content.ReadAsStringAsync().Result;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<TiingoCurrentTopOfBookResponse>));
            List<TiingoCurrentTopOfBookResponse> result = (List<TiingoCurrentTopOfBookResponse>)jsonSerializer.ReadObject(message.Content.ReadAsStreamAsync().Result);
            return result[0];

        }

        public double GetStockPriceForDate(string ticker, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<TiingoStockSearchResponse> SearchStocks(string query)
        {
            
            HttpResponseMessage message = client.GetAsync(URL + "/tiingo/utilities/search?query=" + query + "&token=" + API_KEY).Result;
            message.EnsureSuccessStatusCode();
            string bodyResponse = message.Content.ReadAsStringAsync().Result;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<TiingoStockSearchResponse>));
            List<TiingoStockSearchResponse> result = (List < TiingoStockSearchResponse >)jsonSerializer.ReadObject(message.Content.ReadAsStreamAsync().Result);
            return result;
        }
    }
}
