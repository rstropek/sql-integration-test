using AnimalCounter.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AnimalCounter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalFrequenceStatisticsController : ControllerBase
    {
        private readonly AnimalCounterDbContext context;

        public AnimalFrequenceStatisticsController(AnimalCounterDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "name")] string? nameFilter, [FromQuery(Name = "page")] int? pageIndex)
        {
            try
            {
                return Ok(await context.AnimalFrequencies
                    .Page(pageIndex ?? 1, 10)
                    .FilterByName(nameFilter)
                    .ToArrayAsync());
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }
    }
}
