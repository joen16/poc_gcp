using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_usuario_rol
{
    public long uro_id { get; set; }

    public long usu_id { get; set; }

    public long rol_id { get; set; }

    public bool uro_es_activo { get; set; }

    public DateTime uro_fecha_creacion { get; set; }
}
