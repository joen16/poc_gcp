using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Dto.Request;
using SE_Store_Service;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        ILogger<ProductoController> _logger;
        IGrupoProductoService _grupoProductoService;

        public ProductoController(ILogger<ProductoController> logger,
            IGrupoProductoService grupoProductoService)
        {
            _logger = logger;
            _grupoProductoService = grupoProductoService;
        }
        
    }
}
