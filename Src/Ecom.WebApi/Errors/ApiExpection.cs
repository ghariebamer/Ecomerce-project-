namespace Ecom.WebApi.Errors
{
    public class ApiExpection : CommonResponse
    {
        public ApiExpection(int status, string message = null, string errorDetails = null) : base(status, message)
        {
            ErrorDetails = errorDetails;
        }
        public string ErrorDetails { get; set; }
    }
}
