using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class ClienteDireccionDto
    {
        public long Id { get; set; }
        public ClienteDto Cliente { get; set; }
        public DireccionDto Direccion { get; set; }
        public bool EsActivo { get; set; }
    }
}
