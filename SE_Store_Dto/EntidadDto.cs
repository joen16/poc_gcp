using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class EntidadDto
    {
        public long Id { get; set; }
        public EstadoDto Estado { get; set; }
        public string Nombre { get; set; } 
        public string Codigo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
