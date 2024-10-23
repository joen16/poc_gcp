using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class DireccionDto
    {
        public long Id { get; set; }
        public RegionDto Region { get; set; }
        public ProvinciaDto Provincia { get; set; }
        public DistritoDto Distrito { get; set; }
        public TipoDto Agencia { get; set; }
    }
}
