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
    public class OrdenController : ControllerBase
    {
        IOrdenService _ordenService;
        ILogger<OrdenController> _logger;

        public OrdenController(ILogger<OrdenController> logger,
            IOrdenService ordenService)
        {
            _logger = logger;
            _ordenService = ordenService;
        }

        [PermissionFilter([FuncionalidadEnum.ORDENES, FuncionalidadEnum.VENDER])]
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> SaveAsync(CrearOrdenRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            long idEntidad = this.User.IdEntidad();
            response.Data = await _ordenService.SaveAsync(request, idEntidad);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("guardar-borrador/{idEntidad}")]
        public async Task<IActionResult> SaveBorradorAsync([FromRoute] long idEntidad, CrearOrdenBorradorRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _ordenService.SaveBorradorAsync(request, idEntidad);
            return Ok(response);
        }

        [HttpPost]
        [Route("paginado/{currentPage}/{regPerPage}")]
        public async Task<IActionResult> GetAsync([FromRoute] PaginadoRequest paginado, ObtenerOrdenRequest request)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _ordenService.GetAsync(paginado, request, idEntidad);
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
            response.Data = await _ordenService.GetByIdAsync(id, idEntidad);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{idEntidad}/{numeroOrden}")]
        public async Task<IActionResult> GetByIdAndEntidadAsync([FromRoute] long idEntidad, [FromRoute] string numeroOrden)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _ordenService.GetByNumeroOrdenAsync(idEntidad, numeroOrden);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.ORDENES, FuncionalidadEnum.VENDER])]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            await _ordenService.DeleteAsync(id, idEntidad);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.ORDENES, FuncionalidadEnum.VENDER])]
        [HttpPut]
        [Route("estado/{idOrden}/{idEstado}")]
        public async Task<IActionResult> UpdateEstadoAsync(long idOrden, long idEstado)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            long idEntidad = this.User.IdEntidad();
            await _ordenService.UpdateEstadoAsync(idOrden, idEstado, idEntidad);
            return Ok(response);
        }
    }
}
