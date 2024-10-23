using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Model.EF;
using SE_Store_Service;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        ILogger<EstadoController> _logger;
        IMapper _mapper;
        IEstadoService _estadoService;

        public EstadoController(ILogger<EstadoController> logger, IMapper mapper,
            IEstadoService estadoService)
        {
            _logger = logger;
            _mapper = mapper;
            _estadoService = estadoService;
        }


        [HttpGet]
        [Route("clasificacion/{idClasificacion}")]
        public async Task<IActionResult> GetByClasificacionAsync(long idClasificacion)
        {
            var data = await this._estadoService.GetByClasificacionAsync(idClasificacion);
            return Ok(data);
        }
    }
}
