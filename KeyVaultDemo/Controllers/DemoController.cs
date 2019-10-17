using KeyVaultDemo.Contexts;
using KeyVaultDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyVaultDemo.Controllers
{
    [Route("api/[controller]/books")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly DemoContext _context;
        private readonly IConfiguration _configuration;

        public DemoController(DemoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("secret")]
        public string GetSecret()
        {
            return $"Configuração: {_configuration["Segredo"]}";
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task Insert([FromBody] Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
    }
}