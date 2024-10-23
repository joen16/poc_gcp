using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Service;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        ILogger<RolController> _logger;
        IRolService _rolService;

        public RolController(ILogger<RolController> logger,
            IRolService rolService)
        {
            _logger = logger;
            _rolService = rolService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetByCodigoAsync()
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await this._rolService.GetallAsync();
            return Ok(response);
        }
    }
}
