﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class ProductoDto
    {
        public long Id { get; set; }
        public EntidadDto Entidad { get; set; }
        public EstadoDto Estado { get; set; }
        public TipoDto Marca { get; set; }
        public TipoDto Color { get; set; }
        public TipoDto Talla { get; set; }
        public TipoDto Categoria { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public long Stock { get; set; }
        public DateTime FechaCreacion { get; set; }      
        public List<ProductoDocumentoDto> ListProductoDocumento { get; set; }

    }
}