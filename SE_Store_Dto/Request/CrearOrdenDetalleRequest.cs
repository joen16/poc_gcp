using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class CrearOrdenDetalleRequest
    {
        public long Id { get; set; }
        public long IdProducto { get; set; }
        public long Cantidad { get; set; }
        public long Precio { get; set; }
        public long Total { get; set; }
    }
}
