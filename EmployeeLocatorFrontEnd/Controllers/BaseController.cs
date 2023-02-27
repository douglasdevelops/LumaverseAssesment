using Microsoft.AspNetCore.Mvc;

namespace EmployeeLocatorFrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _config;

        public string BaseAPIUrl { get; set; }

        public BaseController(IConfiguration config)
        {
            _config = config;
            BaseAPIUrl = _config.GetValue<string>("APIBaseUrl");
        }
    }
}