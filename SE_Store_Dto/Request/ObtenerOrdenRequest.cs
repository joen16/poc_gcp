﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Request
{
    public class ObtenerOrdenRequest
    {
        public string? FechaDesde { get; set; }
        public string? FechaHasta { get; set; }
        public long IdEstado { get; set; }
    }
}
