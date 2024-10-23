using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SE_Store_Api.Filters;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Request;
using SE_Store_Helper.Extends;
using SE_Store_Model.EF;
using SE_Store_Service;
using SE_Store_Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        ILogger<TipoController> _logger;
        ITipoService _tipoService;

        public TipoController(ILogger<TipoController> logger, 
            ITipoService tipoService)
        {
            _logger = logger;
            _tipoService = tipoService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("clasificacion/{idClasificacion}")]
        public async Task<IActionResult> GetByClasificacionAsync(long idClasificacion)
        {
            var data = await _tipoService.GetByClasificacionAsync(idClasificacion);

            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = data;
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.VALORES])]
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> SaveAsync(CrearTipoRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            long idEntidad = this.User.IdEntidad();
            response.Data = await _tipoService.SaveAsync(request, idEntidad);
            return Ok(response);
        }

        [HttpPost]
        [Route("paginado/{currentPage}/{regPerPage}")]
        public async Task<IActionResult> GetAsync([FromRoute] PaginadoRequest paginado, ObtenerTipoRequest request)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _tipoService.GetAsync(paginado, request, idEntidad);
            return Ok(response);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            long idEntidad = 1;
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _tipoService.GetByIdAsync(id, idEntidad);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.VALORES])]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            long idEntidad = 1;
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            await _tipoService.DeleteAsync(id, idEntidad);
            return Ok(response);
        }

    }
}
