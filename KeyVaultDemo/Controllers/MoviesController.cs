using KeyVaultDemo.Contexts;
using KeyVaultDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KeyVaultDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DemoContext _context;

        public MoviesController(DemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetMovies()
        {
            return Ok(await _context.Set<Movie>().ToListAsync());
        }
    }
}
