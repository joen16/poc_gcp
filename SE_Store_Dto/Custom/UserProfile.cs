﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Custom
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Nombre { get; set; }
        public long IdUsuario { get; set; }
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
    }
}
