namespace ShoppingCart.Interfaces
{
    public interface IConfig
    {
        string GetCurrencySymbol();
        string GetApiKey();
        string GetApiKeySecret();
        string GetCurrencyLayerBaseUrl();
    }
}
