using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ServerlessAPI.Functions
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;

        public HttpTrigger1(ILogger<HttpTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the "name" query parameter
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string name = query["name"];

            // If no name is provided, use a default value
            name = string.IsNullOrEmpty(name) ? "World" : name;

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Hello, {name}! Welcome to Azure Functions!");

            return response;
        }
    }
}
