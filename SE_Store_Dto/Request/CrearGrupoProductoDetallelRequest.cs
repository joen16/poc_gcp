using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class CrearGrupoProductoDetallelRequest
    {
        public long Id { get; set; }
        public long IdTalla { get; set; }
        public long Stock { get; set; }
    }
}
