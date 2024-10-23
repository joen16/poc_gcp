using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Custom
{
    public class OrdenBorradorDto
    {
        public string IdOrden { get; set; }
        public decimal Monto { get; set; }
        public string IdComercio { get; set; }
        public string IdTransaccion { get; set; }
        public string UrlConfirmacion { get; set; }
        public string UrlLogo { get; set; }
        public string Token { get; set; }

    }
}
