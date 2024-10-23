using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class OrdenProductoDto
    {
        public long Id { get; set; }
        public ProductoDto Producto { get; set; }
        public OrdenDto Orden { get; set; }
        public long Cantidad { get; set; }
        public long Precio { get; set; }
        public decimal Total { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

