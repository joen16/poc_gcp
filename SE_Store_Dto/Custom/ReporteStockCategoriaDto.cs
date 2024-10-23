using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Custom
{
    public class ReporteStockCategoriaDto
    {
        public long IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public List<ReporteStockCategoriaDetalleDto> ListTalla { get; set; }
        public long Total { get; set; }
    }
}
