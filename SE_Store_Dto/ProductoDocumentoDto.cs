using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class ProductoDocumentoDto
    {
        public long Id { get; set; }
        public ProductoDto Producto { get; set; }
        public DocumentoDto Documento { get; set; }
        public bool EsActivo { get; set; }

    }
}
