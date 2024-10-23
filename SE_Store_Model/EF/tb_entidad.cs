using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_entidad
{
    public long ent_id { get; set; }

    public long est_id { get; set; }

    public string ent_nombre { get; set; } = null!;

    public string ent_codigo { get; set; } = null!;

    public DateTime ent_fecha_creacion { get; set; }

    public virtual tb_estado est { get; set; } = null!;

    public virtual ICollection<tb_cliente> tb_cliente { get; set; } = new List<tb_cliente>();

    public virtual ICollection<tb_grupo_producto> tb_grupo_producto { get; set; } = new List<tb_grupo_producto>();

    public virtual ICollection<tb_orden> tb_orden { get; set; } = new List<tb_orden>();

    public virtual ICollection<tb_producto> tb_producto { get; set; } = new List<tb_producto>();

    public virtual ICollection<tb_tipo> tb_tipo { get; set; } = new List<tb_tipo>();

    public virtual ICollection<tb_usuario> tb_usuario { get; set; } = new List<tb_usuario>();
}
