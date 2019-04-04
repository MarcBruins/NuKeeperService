using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NuKeeperService
{
    public static class RepoTrigger
    {
        [FunctionName("RepoTrigger")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var repository = req.Query["repo"];
            var pat = req.Query["pat"];

            var command = $"nukeeper repo {repository} {pat}";

            try
            {
                log.LogInformation(command.Command());
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }

            return (ActionResult) new OkObjectResult($"Finished running command");
        }
    }
}
