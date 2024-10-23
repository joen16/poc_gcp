using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProvinciaDto> ListProvincia { get; set; }
    }
}
