using Microsoft.AspNetCore.Mvc;
using Paraglider.Data.MongoDB;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.API.Controllers
{
    public class TestController : Controller
    {
        public IDataAccess<WeddingComponent> _wedCompDataAccess { get; }

        public TestController(IDataAccess<WeddingComponent> access) 
        {
            _wedCompDataAccess = access;
        }

        [Route("test")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
