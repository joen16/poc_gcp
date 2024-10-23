using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class CrearGrupoProductoRequest
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long IdMarca { get; set; }
        public long IdColor{ get; set; }
        public long IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public long IdDocumento { get; set; }
        public List<CrearGrupoProductoDetallelRequest> Productos { get; set; }

    }
}
