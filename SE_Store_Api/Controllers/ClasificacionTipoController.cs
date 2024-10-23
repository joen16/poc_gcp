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
    public class ClasificacionTipoController : ControllerBase
    {
        IClasificacionTipoService _clasificacionTipoService;
        ILogger<ClasificacionTipoController> _logger;

        public ClasificacionTipoController(ILogger<ClasificacionTipoController> logger,
            IClasificacionTipoService clasificacionTipoService)
        {
            _logger = logger;
            _clasificacionTipoService = clasificacionTipoService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await this._clasificacionTipoService.GetAllAsync();
            return Ok(response);
        }
       
    }
}
