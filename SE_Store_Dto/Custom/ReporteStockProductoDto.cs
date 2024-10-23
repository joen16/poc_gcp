using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Custom
{
    public class ReporteStockProductoDto
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Nombre {  get; set; }
        public List<ReporteStockProductoDetalleDto> ListTalla { get; set; }
        public long Total { get; set; }
    }
}
