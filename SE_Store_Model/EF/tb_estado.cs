using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_estado
{
    public long est_id { get; set; }

    public long ces_id { get; set; }

    public string est_nombre { get; set; } = null!;

    public bool est_es_activo { get; set; }

    public virtual tb_clasificacion_estado ces { get; set; } = null!;

    public virtual ICollection<tb_entidad> tb_entidad { get; set; } = new List<tb_entidad>();

    public virtual ICollection<tb_grupo_producto> tb_grupo_producto { get; set; } = new List<tb_grupo_producto>();

    public virtual ICollection<tb_orden> tb_orden { get; set; } = new List<tb_orden>();

    public virtual ICollection<tb_producto> tb_producto { get; set; } = new List<tb_producto>();

    public virtual ICollection<tb_usuario> tb_usuario { get; set; } = new List<tb_usuario>();
}
