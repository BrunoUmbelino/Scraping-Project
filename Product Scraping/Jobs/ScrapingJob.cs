using Quartz;
using System.Diagnostics;

namespace Product_Scraping.Jobs
{
    public class ScrapingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            return Task.FromResult(true);
        }
    }
}
