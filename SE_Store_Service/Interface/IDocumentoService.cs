using SE_Store_Dto.Custom;
using SE_Store_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE_Store_Dto.Enum;

namespace SE_Store_Service.Interface
{
    public interface IDocumentoService
    {
        Task<DocumentoDto> SaveAsync(FileDto file, TipoDocumentoEnum type);
    }
}
