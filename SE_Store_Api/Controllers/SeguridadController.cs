using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Dto;
using SE_Store_Dto.Request;
using SE_Store_Helper.Encrypt;
using SE_Store_Service;
using SE_Store_Service.Interface;
using System.Security.Permissions;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        ISeguridadService _seguridadService;
        ILogger<SeguridadController> _logger;

        public SeguridadController(ILogger<SeguridadController> logger,
            ISeguridadService seguridadService)
        {
            _logger = logger;
            _seguridadService = seguridadService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _seguridadService.LoginAsync(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 200;
            response.Message = "OK";
            response.Data = await _seguridadService.RefreshTokenAsync(request);
            return Ok(response);
        }
        //[Authorize(PermissionSetAttribute= )]
        [HttpPost]
        [Route("encript")]
        public async Task<IActionResult> Encrupt_test(string cadena)
        {
            return Ok(SecurityH256.Encrypt(cadena));
        }
    }
}
