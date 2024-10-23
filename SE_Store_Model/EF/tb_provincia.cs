using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_provincia
{
    public long prv_id { get; set; }

    public long reg_id { get; set; }

    public string prv_nombre { get; set; } = null!;

    public virtual tb_region reg { get; set; } = null!;

    public virtual ICollection<tb_direccion> tb_direccion { get; set; } = new List<tb_direccion>();

    public virtual ICollection<tb_distrito> tb_distrito { get; set; } = new List<tb_distrito>();
}
