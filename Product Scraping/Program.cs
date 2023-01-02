using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product_Scraping.Jobs;
using Product_Scraping.Models.Database;
using Product_Scraping.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FreeFoodFactsScrapingDatabaseSettings>(
    builder.Configuration.GetSection(nameof(FreeFoodFactsScrapingDatabaseSettings)));

builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<ScrapingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("DemoJob");
    q.AddJob<ScrapingJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DemoJob-trigger")
        .WithCronSchedule("0/5 * * * * ?"));

});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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
