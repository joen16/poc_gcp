using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_password
{
    public int pwd_id { get; set; }

    public long usu_id { get; set; }

    public string pwd_valor { get; set; } = null!;

    public bool pwd_es_activo { get; set; }

    public DateTime pwd_fecha_creacion { get; set; }

    public virtual tb_usuario usu { get; set; } = null!;
}
