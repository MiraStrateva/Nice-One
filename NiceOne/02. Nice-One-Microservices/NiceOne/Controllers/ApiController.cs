namespace NiceOne.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        public const string PathSeparator = "/";
        public const string Id = "{id}";
        public const string Search = "{search}";
    }
}
