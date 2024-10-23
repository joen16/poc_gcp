using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_tipo_documento
{
    public long tdo_id { get; set; }

    public string tdo_nombre { get; set; } = null!;

    public string tdo_path { get; set; } = null!;

    public DateTime tdo_fecha_creacion { get; set; }

    public virtual ICollection<tb_documento> tb_documento { get; set; } = new List<tb_documento>();
}
