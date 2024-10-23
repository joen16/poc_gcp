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
    public class GrupoProductoController : ControllerBase
    {
        IGrupoProductoService _grupoProductoService;
        ILogger<GrupoProductoController> _logger;

        public GrupoProductoController(ILogger<GrupoProductoController> logger,
            IGrupoProductoService grupoProductoService)
        {
            _logger = logger;
            _grupoProductoService = grupoProductoService;
        }

        [PermissionFilter([FuncionalidadEnum.PRODUCTOS])]
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> SaveAsync(CrearGrupoProductoRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            //long idEntidad = 1;
            

            response.Data = await _grupoProductoService.SaveAsync(request, User.IdEntidad());
            return Ok(response);
        }

        [HttpPost]
        [Route("paginado/{currentPage}/{regPerPage}")]
        public async Task<IActionResult> GetAsync([FromRoute] PaginadoRequest paginado, ObtenerGrupoProductoRequest request)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _grupoProductoService.GetAsync(paginado, request, idEntidad);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("paginado-entidad/{entidad}/{currentPage}/{regPerPage}")]
        public async Task<IActionResult> GetByEntidadAsync([FromRoute] long entidad, [FromRoute] PaginadoRequest paginado, ObtenerGrupoProductFilterRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _grupoProductoService.GetAsync(paginado, request, entidad);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            long idEntidad = this.User.IdEntidad(); 
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _grupoProductoService.GetByIdAsync(id, idEntidad);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.PRODUCTOS])]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            await _grupoProductoService.DeleteAsync(id, idEntidad);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("popular/{idEntidad}")]
        public async Task<IActionResult> GetPopularAsync(long idEntidad)
        {
            //long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _grupoProductoService.GetPopularAsync(idEntidad);
            return Ok(response);
        }
    }
}
