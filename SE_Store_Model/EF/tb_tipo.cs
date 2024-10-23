using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_tipo
{
    public long tip_id { get; set; }

    public long cti_id { get; set; }

    public long ent_id { get; set; }

    public string tip_nombre { get; set; } = null!;

    public bool tip_es_activo { get; set; }

    public DateTime tip_fecha_creacion { get; set; }

    public virtual tb_clasificacion_tipo cti { get; set; } = null!;

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual ICollection<tb_direccion> tb_direccion { get; set; } = new List<tb_direccion>();

    public virtual ICollection<tb_grupo_producto> tb_grupo_productotip_id_categoriaNavigation { get; set; } = new List<tb_grupo_producto>();

    public virtual ICollection<tb_grupo_producto> tb_grupo_productotip_id_colorNavigation { get; set; } = new List<tb_grupo_producto>();

    public virtual ICollection<tb_grupo_producto> tb_grupo_productotip_id_marcaNavigation { get; set; } = new List<tb_grupo_producto>();

    public virtual ICollection<tb_producto> tb_productotip_id_categoriaNavigation { get; set; } = new List<tb_producto>();

    public virtual ICollection<tb_producto> tb_productotip_id_colorNavigation { get; set; } = new List<tb_producto>();

    public virtual ICollection<tb_producto> tb_productotip_id_marcaNavigation { get; set; } = new List<tb_producto>();

    public virtual ICollection<tb_producto> tb_productotip_id_tallaNavigation { get; set; } = new List<tb_producto>();
}
