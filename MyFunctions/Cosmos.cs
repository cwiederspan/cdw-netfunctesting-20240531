using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace MyFunctions {

    public class Cosmos {

        private readonly ILogger<Cosmos> Logger;

        public Cosmos(ILogger<Cosmos> logger) {
            this.Logger = logger;
        }

        [Function("Cosmos")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req
        ) {

            var repo = new TableRepository();

            var stopwatch = new Stopwatch();
            var guid = Guid.NewGuid().ToString();

            stopwatch.Start();
            await repo.Create(guid, guid);
            stopwatch.Stop();
            var createMs = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            await repo.Read(guid, guid);
            stopwatch.Stop();
            var readMs = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            await repo.Delete(guid, guid);
            stopwatch.Stop();
            var deleteMs = stopwatch.Elapsed.TotalMilliseconds;

            return new OkObjectResult(new {
                createMs,
                readMs,
                deleteMs
            });
        }
    }
}
