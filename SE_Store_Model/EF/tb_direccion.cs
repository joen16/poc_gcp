using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_direccion
{
    public long dir_id { get; set; }

    public long reg_id { get; set; }

    public long prv_id { get; set; }

    public long dtr_id { get; set; }

    public long tip_id_agencia { get; set; }

    public virtual tb_distrito dtr { get; set; } = null!;

    public virtual tb_provincia prv { get; set; } = null!;

    public virtual tb_region reg { get; set; } = null!;

    public virtual ICollection<tb_cliente_direccion> tb_cliente_direccion { get; set; } = new List<tb_cliente_direccion>();

    public virtual ICollection<tb_orden> tb_orden { get; set; } = new List<tb_orden>();

    public virtual tb_tipo tip_id_agenciaNavigation { get; set; } = null!;
}
