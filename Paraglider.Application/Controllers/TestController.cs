using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.Controllers
{
    public class TestController : Controller
    {
        private readonly IDataAccess<WeddingComponent> _components;
        private readonly IMapper _mapper;

        public TestController(IDataAccess<WeddingComponent> components, IMapper mapper)
        {
            _components = components;
            _mapper = mapper;
        }

        [Route("test")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var components = await _components.GetAllAsync();
            var component = components.FirstOrDefault();
            return Ok(_mapper.Map<PhotographerDTO>(component!));
        }
    }
}
