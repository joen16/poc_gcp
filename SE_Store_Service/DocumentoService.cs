using Google.Apis.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SE_Store_Dto;
using SE_Store_Dto.Custom;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using SE_Store_Helper.Exceptions;
using SE_Store_Helper.File;
using SE_Store_Integration;
using SE_Store_Model.Repository;
using SE_Store_Model.Repository.Interface;
using SE_Store_Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Service
{
    public class DocumentoService : IDocumentoService
    {
        ILogger<DocumentoService> _logger;
        IDocumentoRepository _documentoRepository;
        IGcpService _cpService;
        ITipoDocumentoRepository _tipoDocumentoRepository;

        public DocumentoService(ILogger<DocumentoService> logger,
            IDocumentoRepository documentoRepository, 
            IGcpService cpService,
            ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _logger = logger;
            _documentoRepository = documentoRepository;
            _cpService = cpService;
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }


        public async Task<DocumentoDto> SaveAsync(FileDto file, TipoDocumentoEnum type)
        {
            var tipoDoc = await _tipoDocumentoRepository.GetByIdAsync((int)type);
            if (tipoDoc == null)
            {
                throw new BusinessException(string.Format("Tipo de documento no configurado:{0}", (int)type));
            }

            string fileName =string.Format("{0}/{1}.{2}", tipoDoc.Path, Guid.NewGuid(), FileHelper.GetExtension(file.FileName));
            string url = await _cpService.UploadFileAsync(file, fileName);

            var documentoDto = new DocumentoDto();
            documentoDto.TipoDocumento = new TipoDocumentoDto();
            documentoDto.TipoDocumento.Id = (int)TipoDocumentoEnum.IMAGEN_PRODUCTO;
            documentoDto.Path = url;
            documentoDto.Nombre = file.FileName;
            documentoDto.FechaCreacion = DateTime.Now;
            documentoDto.Peso = file.Length;

            documentoDto = await _documentoRepository.InsertAsync(documentoDto);

            return documentoDto;
        }

       
    }
}
