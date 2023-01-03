using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product_Scraping.Jobs;
using Product_Scraping.Models.Database;
using Product_Scraping.Services;
using Quartz;

internal class Program
{
    private static void Main(string[] args)
    {
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
            q.UseMicrosoftDependencyInjectionJobFactory();
            var jobKey = new JobKey("ScrapingJob");
            q.AddJob<ScrapingJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("ScrapingJob-trigger")
                .WithCronSchedule("0 * * ? * *")); /// Cron expression 0 0 2 ? * * *
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
    }
}