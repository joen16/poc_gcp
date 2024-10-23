using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Model.EF;
using SE_Store_Service;
using SE_Store_Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        ILogger<PagoController> _logger;
        IMapper _mapper;
        IPagoService _pagoService;

        public PagoController(ILogger<PagoController> logger,
            IMapper mapper,
            IPagoService pagoService)
        {
            _logger = logger;
            _mapper = mapper;
            _pagoService = pagoService;
        }


        [HttpPost]
        [Route("confirm-pay")]
        public async Task<IActionResult> ConfirmPayAsync(string ipn)
        {
            _logger.LogDebug(ipn);
            return Ok();
        }

        [HttpPut]
        [Route("confirm-pay")]
        public async Task<IActionResult> ConfirmPayAsync3(string ipn)
        {
            _logger.LogDebug(ipn);
            return Ok();
        }

        [HttpGet]
        [Route("confirm-pay")]
        public async Task<IActionResult> ConfirmPayAsync2(string ipn)
        {
            _logger.LogDebug(ipn);
            return Ok();
        }


        [HttpPost]
        [Route("create-token")]
        public async  Task<IActionResult> CreateTokenAsync(CrearTokenRequest request)
        {
            var result = await _pagoService.GenerateToken(request.TransactionId, request.OrderNumber, request.Monto);
            return Ok(result);
        }


    }
}
