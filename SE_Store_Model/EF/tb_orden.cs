using System;
using System.Collections.Generic;

namespace SE_Store_Model.EF;

public partial class tb_orden
{
    public long ord_id { get; set; }

    public long ent_id { get; set; }

    public long est_id { get; set; }

    public long cli_id { get; set; }

    public long can_id { get; set; }

    public long? dir_id { get; set; }

    public decimal ord_monto_sub_total { get; set; }

    public decimal ord_monto_descuento { get; set; }

    public decimal ord_monto_total { get; set; }

    public DateTime ord_fecha_creacion { get; set; }

    public long ord_cantidad { get; set; }

    public bool ord_es_envio { get; set; }

    public bool ord_es_web_pay { get; set; }

    public decimal ord_monto_envio { get; set; }

    public decimal? ord_monto_pagado { get; set; }

    public decimal? ord_wp_monto_comision_porc { get; set; }

    public decimal? ord_wp_monto_comision_fija { get; set; }

    public decimal? ord_wp_monto_igv { get; set; }

    public decimal? ord_wp_monto_comision_total { get; set; }

    public decimal? ord_wp_monto_comercio_total { get; set; }

    public string? ord_numero_orden { get; set; }

    public virtual tb_canal can { get; set; } = null!;

    public virtual tb_cliente cli { get; set; } = null!;

    public virtual tb_direccion? dir { get; set; }

    public virtual tb_entidad ent { get; set; } = null!;

    public virtual tb_estado est { get; set; } = null!;

    public virtual ICollection<tb_orden_producto> tb_orden_producto { get; set; } = new List<tb_orden_producto>();
}
