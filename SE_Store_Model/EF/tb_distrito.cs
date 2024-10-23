using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_distrito
{
    public long dtr_id { get; set; }

    public long prv_id { get; set; }

    public string dtr_nombre { get; set; } = null!;

    public virtual tb_provincia prv { get; set; } = null!;

    public virtual ICollection<tb_direccion> tb_direccion { get; set; } = new List<tb_direccion>();
}
