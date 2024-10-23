using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_region
{
    public long reg_id { get; set; }

    public string reg_nombre { get; set; } = null!;

    public virtual ICollection<tb_direccion> tb_direccion { get; set; } = new List<tb_direccion>();

    public virtual ICollection<tb_provincia> tb_provincia { get; set; } = new List<tb_provincia>();
}
