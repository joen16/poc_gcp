using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class ClienteDto
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public EntidadDto Entidad { get; set; }
        public List<ClienteDireccionDto> ListClienteDireccion { get; set; }
        public List<OrdenDto> ListOrden { get; set; }

    }
}
