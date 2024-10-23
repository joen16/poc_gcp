using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
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
    public class RegionController : ControllerBase
    {
        ILogger<RegionController> _logger;
        IRegionService _regionService;

        public RegionController(ILogger<RegionController> logger,
            IRegionService regionService)
        {
            _logger = logger;
            _regionService = regionService;
        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _regionService.GetAllAsync();
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list-with-relations")]
        public async Task<IActionResult> GetAllWithRelationsAsync()
        {
            var data = await _regionService.GetAllWithRelationsAsync();
            return Ok(data);
        }
    }
}
