using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Paraglider.Data.MongoDB;

namespace Paraglider.API.Controllers
{
    public class TestController : Controller
    {
        public TestController(IMongoDatabase db, WeddingComponentDataAccess access) 
        {
            var chec = true;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
