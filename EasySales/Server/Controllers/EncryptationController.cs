using EasySales.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptationController : ControllerBase
    {
        private readonly IEncryptationRepository encryptationRepository;

        public EncryptationController(IEncryptationRepository encryptationRepository)
        {
            this.encryptationRepository = encryptationRepository;
        }

        [HttpGet]
        [Route("encriptar/{data}")]
        public async Task<ActionResult> Encrypt(string data)
        {
            try
            {
                string dataEcrypted = encryptationRepository.Encrypt(data);
                return Ok(dataEcrypted);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error encriptando data desde el servidor");
            }
        }

        [HttpGet]
        [Route("desencriptar/{data}")]
        public async Task<ActionResult> Decrypt(string data)
        {
            try
            {
                string dataDecrypted = encryptationRepository.Decrypt(data);
                return Ok(dataDecrypted);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error desencriptando data desde el servidor");
            }
        }

    }
}
