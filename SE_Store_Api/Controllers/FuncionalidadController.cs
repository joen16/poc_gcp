using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Api.Filters;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Request;
using SE_Store_Helper.Extends;
using SE_Store_Service;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionalidadController : ControllerBase
    {
        IFuncionalidadService _funcionalidadervice;
        ILogger<FuncionalidadController> _logger;

        public FuncionalidadController(ILogger<FuncionalidadController> logger,
            IFuncionalidadService funcionalidadervice)
        {
            _logger = logger;
            _funcionalidadervice = funcionalidadervice;
        }

        [HttpGet]
        [Route("rol")]
        public async Task<IActionResult> GetAsync()
        {
            long idEntidad = User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _funcionalidadervice.GetByRolAsync(idEntidad);
            return Ok(response);
        }

        
    }
}
