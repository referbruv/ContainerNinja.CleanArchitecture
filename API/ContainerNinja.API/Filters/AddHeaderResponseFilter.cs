using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContainerNinja.API.Filters
{
    public class AddHandlerHostHeaderResponseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // let the request be handled
            var executedContext = await next();

            // add a custom response header for all successful requests
            var hostName = System.Net.Dns.GetHostName();
            var response = executedContext.HttpContext.Response;
            
            response.Headers.Add("X-Handler-HostName", new[] { hostName });
        }
    }
}