using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Helper.Extends;
using SE_Store_Service;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        IClienteService _clienteService;
        ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger,
            IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetByCodigoAsync([FromQuery] string codigo)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await this._clienteService.GetByCodigoAsync(codigo, idEntidad);
            return Ok(response);
        }
        
    }
}
