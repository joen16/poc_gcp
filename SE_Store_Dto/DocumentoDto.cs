using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class DocumentoDto
    {
        public long Id { get; set; }
        public TipoDocumentoDto TipoDocumento { get; set; }
        public string Nombre { get; set; }
        public string Path { get; set; }
        public long Peso { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
