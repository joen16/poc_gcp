﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class RolDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public List<RolFuncionalidadDto> ListRolFuncionalidad { get; set; }
    }
}
