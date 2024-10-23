using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Api.Filters;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Enum;
using SE_Store_Service.Interface;

namespace SE_Store_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        ILogger<DocumentoController> _logger;
        IDocumentoService _documentoService;

        public DocumentoController(ILogger<DocumentoController> logger,
            IDocumentoService documentoService)
        {
            _logger = logger;
            _documentoService = documentoService;
        }

        [PermissionFilter([FuncionalidadEnum.PRODUCTOS])]
        [HttpPost]
        [Route("upload/tipo/{type}")]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file, long type)
        {
            ResponseEntity response = new ResponseEntity();
            response.Code = 500;
            response.Message = "Archivo no pudo ser subido";

            if (file != null && file.Length > 0)
            {
                FileDto fileDto = new FileDto();
                fileDto.FileName= file.FileName;
                fileDto.Length = file.Length;
                fileDto.ContentDisposition = file.ContentDisposition;
                fileDto.ContentType = file.ContentType;
                fileDto.Name = file.Name;


                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileDto.Data = memoryStream.ToArray();
                }
                TipoDocumentoEnum enumValue = (TipoDocumentoEnum)type;
                 var documentoDto = await _documentoService.SaveAsync(fileDto, enumValue);

                response.Code = 200;
                response.Message = "OK";
                response.Data = documentoDto;

                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
