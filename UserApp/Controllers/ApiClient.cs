namespace UserApp.Controllers
{
    public class ApiClient : HttpClient
    {
        public ApiClient() : base() 
        { 
            base.DefaultRequestHeaders.Add("api-key", "a69b79ff-8637-4801-b3b4-0ed3de6a714a");
        }
    }
}
