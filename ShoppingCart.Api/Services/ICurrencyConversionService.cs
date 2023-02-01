namespace ShoppingCart.Services
{
    public interface ICurrencyConversionService
    {
        Task<double> Convert(string currencyCode, double value);
    }
}
