using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace FunctionApp1
{
    public static class Function1
    {
        public const string AzureWebJobsStorage = "Your Key";
        [FunctionName("Table_GetTodos")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req,
            [Table("TestCreatetbl", Connection = "AzureWebJobsStorage")] CloudTable thankfulfor, TraceWriter log)
        {
            log.Info("Getting todo list items");
            var query = new TableQuery<TestCreatetbl>();
            var segment = await thankfulfor.ExecuteQuerySegmentedAsync(query, null);

            return segment == null
               ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
               : req.CreateResponse(HttpStatusCode.OK, "Hello " + segment);
            
        }
    }
}
