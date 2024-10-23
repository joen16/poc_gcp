using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_usuario
{
    public long usu_id { get; set; }

    public long est_id { get; set; }

    public long ent_id { get; set; }

    public long rol_id { get; set; }

    public string usu_email { get; set; } = null!;

    public string usu_nombre { get; set; } = null!;

    public DateTime usu_fecha_creacion { get; set; }

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual tb_estado est { get; set; } = null!;

    public virtual tb_rol rol { get; set; } = null!;

    public virtual ICollection<tb_password> tb_password { get; set; } = new List<tb_password>();
}
