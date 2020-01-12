using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        public string Get()
        {
            Console.WriteLine($"Request: {DateTime.Now}");
            var jobFireForget = BackgroundJob.Enqueue(() => Debug.WriteLine($"Fire and forget: {DateTime.Now}"));
            var jobDelayed = BackgroundJob.Schedule(() => Debug.WriteLine($"Aguarde (Delay): {DateTime.Now}"), TimeSpan.FromSeconds(30));
            BackgroundJob.ContinueWith(jobDelayed, () => Debug.WriteLine($"Continuação: {DateTime.Now}"));
            RecurringJob.AddOrUpdate(() => Debug.WriteLine($"Recorrente: {DateTime.Now}"), Cron.Minutely);
            return "Jobs criados com sucesso!";
        }
    }
}