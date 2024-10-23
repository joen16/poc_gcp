using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class ObtenerGrupoProductFilterRequest
    {
        public long[] IdCategoria { get; set; }
        public long[] IdMarca { get; set; }
        public long[] IdColor { get; set; }
    }
}
