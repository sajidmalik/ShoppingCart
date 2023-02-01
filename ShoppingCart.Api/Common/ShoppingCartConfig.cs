using System.Configuration;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Common
{
    public class ShoppingCartConfig : IConfig
    {
        IConfiguration _config;

        public ShoppingCartConfig(IConfiguration config)
        {
            _config = config;
        }

        public string GetApiKey()
        {
            return _config["Settings:CurrencyLayer:ApiKey"];
        }

        public string GetApiKeySecret()
        {
            return _config["Settings:CurrencyLayer:ApiKeySecret"];
        }

        public string GetCurrencyLayerBaseUrl()
        {
            return _config["Settings:CurrencyLayer:CurrencyLayerBaseUrl"];
        }

        public string GetCurrencySymbol()
        {
            return _config["Settings:CurrencyCode"];
        }
    }
}
