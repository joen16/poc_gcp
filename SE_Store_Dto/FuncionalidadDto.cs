using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class FuncionalidadDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Path { get; set; }
        public string Orden { get; set; }
        public string Icon { get; set; }
        public ModuloDto Modulo { get; set; }
    }
}
