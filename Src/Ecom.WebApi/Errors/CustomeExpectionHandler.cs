namespace Ecom.WebApi.Errors
{
    public static class CustomeExpectionHandler
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExpectionMiddleware>();
        }
    }
}
