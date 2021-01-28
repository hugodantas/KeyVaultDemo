using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KeyVaultDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SecretController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return "Secret controller is working";
        }

        [HttpGet("{secret}")]
        public ActionResult GetSecret(string secret)
        {
            var securedSecret = _configuration[secret];

            if (string.IsNullOrEmpty(securedSecret))
                return NotFound($"Secret not found with key {secret}");

            return Ok($"The secret is '{securedSecret}'");
        }
    }
}
