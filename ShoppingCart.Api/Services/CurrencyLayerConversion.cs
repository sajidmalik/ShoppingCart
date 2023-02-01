using Newtonsoft.Json;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Services
{
    public class CurrencyLayerConversion : ICurrencyConversionService
    {
        private HttpClient _httpClient;
        private readonly IConfig _config;

        private readonly string _baseUrl;

        public CurrencyLayerConversion(IConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            _config = config;

            _baseUrl = _config.GetCurrencyLayerBaseUrl();
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add(_config.GetApiKey(), _config.GetApiKeySecret());
        }

        public async Task<double> Convert(string currencyCode, double value)
        {
            if (currencyCode == null) throw new ArgumentNullException(nameof(currencyCode));

            string url = $"{_baseUrl}?to={currencyCode}&from={_config.GetCurrencySymbol()}&amount={value}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                var content = response.Content.ReadAsStringAsync();
                var data = content.Result;

                var result =  JsonConvert.DeserializeObject<JsonObject>(data).Result;
                return result;
             
            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }
    }

    public class JsonObject
    {
        public double Result { get; set; }
        public Info Info { get; set; }
        public Query Query { get; set; }
    }
    public class Info
    {
        public double Rate { get; set; }
        public string TimeStamp { get; set; }
    }
    public class Query
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
    }
}
