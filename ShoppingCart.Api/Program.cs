using ShoppingCart.Services;
using ShoppingCart.Db;
using ShoppingCart.Interfaces;
using ShoppingCart.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ICurrencyConversionService, CurrencyLayerConversion>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IConfig, ShoppingCartConfig>();
builder.Services.AddScoped<IShoppingBasksetContext, ShoppingBasketContext>();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.Run();