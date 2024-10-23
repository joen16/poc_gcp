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
    public class ReporteController : ControllerBase
    {

        IReporteService _reporteService;
        ILogger<ReporteController> _logger;

        public ReporteController(ILogger<ReporteController> logger,
            IReporteService reporteService)
        {
            _logger = logger;
            _reporteService = reporteService;
        }

        [PermissionFilter([FuncionalidadEnum.REPORTE_STOCK_CATEGORIA])]
        [HttpGet]
        [Route("stock/categoria")]
        public async Task<IActionResult> GetStockPorCategoriaAsync()
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _reporteService.GetStockPorCategoriaAsync(idEntidad);
            return Ok(response);
        }

        [PermissionFilter([FuncionalidadEnum.REPORTE_STOCK_PRODUCTO])]
        [HttpGet]
        [Route("stock/producto")]
        public async Task<IActionResult> GetStockPorProductoAsync()
        {
            long idEntidad = this.User.IdEntidad();
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _reporteService.GetStockPorProductoAsync(idEntidad);
            return Ok(response);
        }
    }
}
