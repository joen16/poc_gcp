using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class TipoDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public EntidadDto Entidad { get; set; }
        public ClasificacionTipoDto Clasificacion { get; set; }
    }
}
