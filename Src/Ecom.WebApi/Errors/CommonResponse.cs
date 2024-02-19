
namespace Ecom.WebApi.Errors
{
    public class CommonResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public CommonResponse(int status,string message =null)
        {
            StatusCode = status;
            Message = message??GetSpecficMessage(StatusCode);
        }

        private string GetSpecficMessage(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            4001 => "Not Authorize",
            404 => "Resource Not Found ",
            500 => "Internal Server Error"
        };


}
}
