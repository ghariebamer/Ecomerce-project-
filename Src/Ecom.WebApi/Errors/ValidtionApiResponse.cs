namespace Ecom.WebApi.Errors
{
    public class ValidtionApiResponse : CommonResponse
    {
        public ValidtionApiResponse() : base(400)
        {
        }
        public IEnumerable<string > Errors { get; set; }=new List<string>();
    }
}
