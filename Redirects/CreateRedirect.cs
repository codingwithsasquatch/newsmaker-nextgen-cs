
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using NewsMaker.Models;
using System.Linq;

namespace NewsMaker.Redirects
{
    public static class CreateRedirect
    {
        [FunctionName("CreateRedirect")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req,
            [Table("RedirectUrls")] CloudTable redirectUrls,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string body = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(body);

            RedirectUrl newUrl = new RedirectUrl();
            newUrl.LongUrl = data?.longUrl;
            newUrl.ShortUrl = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(newUrl.LongUrl));
            newUrl.PartitionKey = newUrl.ShortUrl.Substring(0,1);

            if (newUrl.ShortUrl != null)
            {
                log.Info($"url value: {newUrl.ShortUrl}");
                var upsertOperation = TableOperation.InsertOrReplace(newUrl);
                var upsertResult = await redirectUrls.ExecuteAsync(upsertOperation);
                return new OkObjectResult(newUrl.ShortUrl);
            } else {
                return new BadRequestObjectResult("Bad Request");
            }
        }
    }
}
