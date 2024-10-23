using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class CrearOrdenRequest
    {
        public long Id { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal MontoEnvio { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoSubTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public long Cantidad { get; set; }
        public bool EsEnvio { get; set; }
        public bool EsWebPay { get; set; }
        public long CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public long TelefonoCliente { get; set; }
        public string? EmailCliente { get; set; }
        public long IdRegion { get; set; }
        public long IdProvincia { get; set; }
        public long IdDistrito { get; set; }
        public long IdAgencia { get; set; }
        public long IdCanal { get; set; }
        public long IdDireccion { get; set; }
        public List<CrearOrdenDetalleRequest> Productos { get; set; }

    }
}
