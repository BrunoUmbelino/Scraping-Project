using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product_Scraping.Models.Database;
using Product_Scraping.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FreeFoodFactsScrapingDatabaseSettings>(
    builder.Configuration.GetSection(nameof(FreeFoodFactsScrapingDatabaseSettings)));

builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<ScrapingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
