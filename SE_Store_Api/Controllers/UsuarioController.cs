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
    public class UsuarioController : ControllerBase
    {
        IUsuarioService _usuarioService;
        ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger,
            IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [PermissionFilter([FuncionalidadEnum.USUARIOS])]
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> SaveAsync(CrearUsuarioRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            long idEntidad = this.User.IdEntidad();
            response.Data = await _usuarioService.SaveAsync(request, idEntidad);
            return Ok(response);
        }

        [HttpPost]
        [Route("paginado/{currentPage}/{regPerPage}")]
        public async Task<IActionResult> GetAsync([FromRoute] PaginadoRequest paginado, ObtenerUsuarioRequest request)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _usuarioService.GetAsync(paginado, request, idEntidad);
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
            response.Data = await _usuarioService.GetByIdAsync(id);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.USUARIOS])]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            await _usuarioService.DeleteAsync(id, idEntidad);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.USUARIOS])]
        [HttpPut]
        [Route("estado/{idUsuario}/{idEstado}")]
        public async Task<IActionResult> UpdateEstadoAsync(long idUsuario, long idEstado)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            long idEntidad = this.User.IdEntidad();
            await _usuarioService.UpdateEstadoAsync(idUsuario, idEstado, idEntidad);
            return Ok(response);
        }
    }
}
