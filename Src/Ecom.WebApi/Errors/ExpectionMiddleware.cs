using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Ecom.WebApi.Errors
{
    public  class ExpectionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly IHostEnvironment environment;

        public ExpectionMiddleware(RequestDelegate requestDelegate, IHostEnvironment environment)
        {
            this.requestDelegate = requestDelegate;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
                //logger.LogInformation("success");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = environment.IsDevelopment()?
                    new ApiExpection(context.Response.StatusCode, ex.Message, ex.StackTrace) :
                    new ApiExpection(context.Response.StatusCode);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);

            }
        }
        
    }
}
