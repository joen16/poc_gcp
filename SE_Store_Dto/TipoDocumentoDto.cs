using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class TipoDocumentoDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Path { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<DocumentoDto> ListDocumento { get; set; }
    }
}
