
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using NewsMaker.Models;
using System.Threading.Tasks;

namespace NewsMaker.Redirects
{
    public static class Go
    {
        public static readonly string FALLBACK_URL = System.Environment.GetEnvironmentVariable("FALLBACK_URL");

        [FunctionName("Go")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{shortUrl}")] HttpRequest req,
            [Table("RedirectUrls")] CloudTable redirectUrls,
            string shortUrl,
            TraceWriter log)
        {
            var retrieveOperation = TableOperation.Retrieve<RedirectUrl>(shortUrl.Substring(0,1), shortUrl);
            var retrieveResult = await redirectUrls.ExecuteAsync(retrieveOperation);
            var url = (RedirectUrl)(retrieveResult.Result);
            return url != null
                ? new RedirectResult(url.LongUrl)
                : new RedirectResult(FALLBACK_URL);
        }
    }
}
