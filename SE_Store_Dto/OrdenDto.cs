using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class OrdenDto
    {
        public long Id { get; set; }
        public string NumeroOrden { get; set; }
        public EntidadDto Entidad { get; set; }
        public EstadoDto Estado { get; set; }
        public CanalDto Canal { get; set; }
        public ClienteDto Cliente { get; set; }
        public DireccionDto Direccion { get; set; }
        public decimal MontoEnvio { get; set; }
        public decimal MontoSubTotal { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal WpMontoComisionPorc { get; set; }
        public decimal WpMontoComisionFija { get; set; }
        public decimal WpMontoIgv { get; set; }
        public decimal WpMontoComisionTotal { get; set; }
        public decimal WpMontoComercioTotal { get; set; }
        public long Cantidad { get; set; }
        public bool EsEnvio { get; set; }
        public bool EsWebPay { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<OrdenProductoDto> ListOrdenProducto { get; set; }

    }
}
