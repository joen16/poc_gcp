using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SE_Store_Model.EF;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tb_canal> tb_canal { get; set; }

    public virtual DbSet<tb_clasificacion_estado> tb_clasificacion_estado { get; set; }

    public virtual DbSet<tb_clasificacion_tipo> tb_clasificacion_tipo { get; set; }

    public virtual DbSet<tb_cliente> tb_cliente { get; set; }

    public virtual DbSet<tb_cliente_direccion> tb_cliente_direccion { get; set; }

    public virtual DbSet<tb_direccion> tb_direccion { get; set; }

    public virtual DbSet<tb_distrito> tb_distrito { get; set; }

    public virtual DbSet<tb_documento> tb_documento { get; set; }

    public virtual DbSet<tb_entidad> tb_entidad { get; set; }

    public virtual DbSet<tb_estado> tb_estado { get; set; }

    public virtual DbSet<tb_funcionalidad> tb_funcionalidad { get; set; }

    public virtual DbSet<tb_grupo_producto> tb_grupo_producto { get; set; }

    public virtual DbSet<tb_modulo> tb_modulo { get; set; }

    public virtual DbSet<tb_orden> tb_orden { get; set; }

    public virtual DbSet<tb_orden_producto> tb_orden_producto { get; set; }

    public virtual DbSet<tb_parametro> tb_parametro { get; set; }

    public virtual DbSet<tb_password> tb_password { get; set; }

    public virtual DbSet<tb_producto> tb_producto { get; set; }

    public virtual DbSet<tb_producto_documento> tb_producto_documento { get; set; }

    public virtual DbSet<tb_provincia> tb_provincia { get; set; }

    public virtual DbSet<tb_region> tb_region { get; set; }

    public virtual DbSet<tb_rol> tb_rol { get; set; }

    public virtual DbSet<tb_rol_funcionalidad> tb_rol_funcionalidad { get; set; }

    public virtual DbSet<tb_tipo> tb_tipo { get; set; }

    public virtual DbSet<tb_tipo_documento> tb_tipo_documento { get; set; }

    public virtual DbSet<tb_usuario> tb_usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=store;user id=root;password=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tb_canal>(entity =>
        {
            entity.HasKey(e => e.can_id).HasName("PRIMARY");

            entity.Property(e => e.can_nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<tb_clasificacion_estado>(entity =>
        {
            entity.HasKey(e => e.ces_id).HasName("PRIMARY");

            entity.Property(e => e.ces_nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<tb_clasificacion_tipo>(entity =>
        {
            entity.HasKey(e => e.cti_id).HasName("PRIMARY");

            entity.Property(e => e.cti_nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<tb_cliente>(entity =>
        {
            entity.HasKey(e => e.cli_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ent_id, "fk_cli_ent_id_idx");

            entity.Property(e => e.cli_codigo).HasMaxLength(45);
            entity.Property(e => e.cli_email).HasMaxLength(100);
            entity.Property(e => e.cli_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.cli_nombre).HasMaxLength(45);

            entity.HasOne(d => d.ent).WithMany(p => p.tb_cliente)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cli_ent_id");
        });

        modelBuilder.Entity<tb_cliente_direccion>(entity =>
        {
            entity.HasKey(e => e.cld_id).HasName("PRIMARY");

            entity.HasIndex(e => e.cli_id, "fk_cld_cli_id_idx");

            entity.HasIndex(e => e.dir_id, "fk_cld_dir_id_idx");

            entity.HasOne(d => d.cli).WithMany(p => p.tb_cliente_direccion)
                .HasForeignKey(d => d.cli_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cld_cli_id");

            entity.HasOne(d => d.dir).WithMany(p => p.tb_cliente_direccion)
                .HasForeignKey(d => d.dir_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cld_dir_id");
        });

        modelBuilder.Entity<tb_direccion>(entity =>
        {
            entity.HasKey(e => e.dir_id).HasName("PRIMARY");

            entity.HasIndex(e => e.dtr_id, "fk_dir_dtr_id_idx");

            entity.HasIndex(e => e.prv_id, "fk_dir_prv_id_idx");

            entity.HasIndex(e => e.reg_id, "fk_dir_reg_id_idx");

            entity.HasIndex(e => e.tip_id_agencia, "fk_dir_tip_id_agencia_idx");

            entity.HasOne(d => d.dtr).WithMany(p => p.tb_direccion)
                .HasForeignKey(d => d.dtr_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dir_dtr_id");

            entity.HasOne(d => d.prv).WithMany(p => p.tb_direccion)
                .HasForeignKey(d => d.prv_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dir_prv_id");

            entity.HasOne(d => d.reg).WithMany(p => p.tb_direccion)
                .HasForeignKey(d => d.reg_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dir_reg_id");

            entity.HasOne(d => d.tip_id_agenciaNavigation).WithMany(p => p.tb_direccion)
                .HasForeignKey(d => d.tip_id_agencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dir_tip_id_agencia");
        });

        modelBuilder.Entity<tb_distrito>(entity =>
        {
            entity.HasKey(e => e.dtr_id).HasName("PRIMARY");

            entity.HasIndex(e => e.prv_id, "fk_dtr_prv_id_idx");

            entity.Property(e => e.dtr_nombre).HasMaxLength(45);

            entity.HasOne(d => d.prv).WithMany(p => p.tb_distrito)
                .HasForeignKey(d => d.prv_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dtr_prv_id");
        });

        modelBuilder.Entity<tb_documento>(entity =>
        {
            entity.HasKey(e => e.doc_id).HasName("PRIMARY");

            entity.HasIndex(e => e.tdo_id, "fk_doc_tdo_id_idx");

            entity.Property(e => e.doc_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.doc_nombre).HasMaxLength(100);
            entity.Property(e => e.doc_path).HasMaxLength(200);

            entity.HasOne(d => d.tdo).WithMany(p => p.tb_documento)
                .HasForeignKey(d => d.tdo_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doc_tdo_id");
        });

        modelBuilder.Entity<tb_entidad>(entity =>
        {
            entity.HasKey(e => e.ent_id).HasName("PRIMARY");

            entity.HasIndex(e => e.est_id, "fk_ent_est_id_idx");

            entity.Property(e => e.ent_codigo).HasMaxLength(45);
            entity.Property(e => e.ent_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.ent_nombre).HasMaxLength(45);

            entity.HasOne(d => d.est).WithMany(p => p.tb_entidad)
                .HasForeignKey(d => d.est_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ent_est_id");
        });

        modelBuilder.Entity<tb_estado>(entity =>
        {
            entity.HasKey(e => e.est_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ces_id, "fk_est_ces_id_idx");

            entity.Property(e => e.est_nombre).HasMaxLength(45);

            entity.HasOne(d => d.ces).WithMany(p => p.tb_estado)
                .HasForeignKey(d => d.ces_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_est_ces_id");
        });

        modelBuilder.Entity<tb_funcionalidad>(entity =>
        {
            entity.HasKey(e => e.fun_id).HasName("PRIMARY");

            entity.HasIndex(e => e.mod_id, "tb_funcionalidad_tb_modulo_FK");

            entity.Property(e => e.fun_icon).HasMaxLength(100);
            entity.Property(e => e.fun_nombre).HasMaxLength(100);
            entity.Property(e => e.fun_orden).HasMaxLength(100);
            entity.Property(e => e.fun_path).HasMaxLength(100);

            entity.HasOne(d => d.mod).WithMany(p => p.tb_funcionalidad)
                .HasForeignKey(d => d.mod_id)
                .HasConstraintName("tb_funcionalidad_tb_modulo_FK");
        });

        modelBuilder.Entity<tb_grupo_producto>(entity =>
        {
            entity.HasKey(e => e.grp_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ent_id, "fk_grp_ent_id_idx");

            entity.HasIndex(e => e.est_id, "fk_grp_est_id_idx");

            entity.HasIndex(e => e.tip_id_categoria, "fk_grp_tip_id_categoria_idx");

            entity.HasIndex(e => e.tip_id_color, "fk_grp_tip_id_color_idx");

            entity.HasIndex(e => e.tip_id_marca, "fk_grp_tip_id_marca_idx");

            entity.Property(e => e.grp_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.grp_nombre).HasMaxLength(45);
            entity.Property(e => e.grp_precio).HasPrecision(12);

            entity.HasOne(d => d.ent).WithMany(p => p.tb_grupo_producto)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grp_ent_id");

            entity.HasOne(d => d.est).WithMany(p => p.tb_grupo_producto)
                .HasForeignKey(d => d.est_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grp_est_id");

            entity.HasOne(d => d.tip_id_categoriaNavigation).WithMany(p => p.tb_grupo_productotip_id_categoriaNavigation)
                .HasForeignKey(d => d.tip_id_categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grp_tip_id_categoria");

            entity.HasOne(d => d.tip_id_colorNavigation).WithMany(p => p.tb_grupo_productotip_id_colorNavigation)
                .HasForeignKey(d => d.tip_id_color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grp_tip_id_color");

            entity.HasOne(d => d.tip_id_marcaNavigation).WithMany(p => p.tb_grupo_productotip_id_marcaNavigation)
                .HasForeignKey(d => d.tip_id_marca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grp_tip_id_marca");
        });

        modelBuilder.Entity<tb_modulo>(entity =>
        {
            entity.HasKey(e => e.mod_id).HasName("PRIMARY");

            entity.Property(e => e.mod_nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<tb_orden>(entity =>
        {
            entity.HasKey(e => e.ord_id).HasName("PRIMARY");

            entity.HasIndex(e => e.can_id, "fk_ord_can_id_idx");

            entity.HasIndex(e => e.cli_id, "fk_ord_cli_id_idx");

            entity.HasIndex(e => e.dir_id, "fk_ord_dir_id_idx");

            entity.HasIndex(e => e.ent_id, "fk_ord_ent_id_idx");

            entity.HasIndex(e => e.est_id, "fk_ord_est_id_idx");

            entity.Property(e => e.ord_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.ord_monto_descuento).HasPrecision(12);
            entity.Property(e => e.ord_monto_envio).HasPrecision(12);
            entity.Property(e => e.ord_monto_pagado).HasPrecision(12);
            entity.Property(e => e.ord_monto_sub_total).HasPrecision(12);
            entity.Property(e => e.ord_monto_total).HasPrecision(12);
            entity.Property(e => e.ord_numero_orden).HasMaxLength(40);
            entity.Property(e => e.ord_wp_monto_comercio_total).HasPrecision(12);
            entity.Property(e => e.ord_wp_monto_comision_fija).HasPrecision(12);
            entity.Property(e => e.ord_wp_monto_comision_porc).HasPrecision(12);
            entity.Property(e => e.ord_wp_monto_comision_total).HasPrecision(12);
            entity.Property(e => e.ord_wp_monto_igv).HasPrecision(12);

            entity.HasOne(d => d.can).WithMany(p => p.tb_orden)
                .HasForeignKey(d => d.can_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ord_can_id");

            entity.HasOne(d => d.cli).WithMany(p => p.tb_orden)
                .HasForeignKey(d => d.cli_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ord_cli_id");

            entity.HasOne(d => d.dir).WithMany(p => p.tb_orden)
                .HasForeignKey(d => d.dir_id)
                .HasConstraintName("fk_ord_dir_id");

            entity.HasOne(d => d.ent).WithMany(p => p.tb_orden)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ord_ent_id");

            entity.HasOne(d => d.est).WithMany(p => p.tb_orden)
                .HasForeignKey(d => d.est_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ord_est_id");
        });

        modelBuilder.Entity<tb_orden_producto>(entity =>
        {
            entity.HasKey(e => e.opr_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ord_id, "fk_opr_ord_id_idx");

            entity.HasIndex(e => e.pro_id, "fk_opr_pro_id_idx");

            entity.Property(e => e.opr_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.opr_total).HasPrecision(12);

            entity.HasOne(d => d.ord).WithMany(p => p.tb_orden_producto)
                .HasForeignKey(d => d.ord_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_opr_ord_id");

            entity.HasOne(d => d.pro).WithMany(p => p.tb_orden_producto)
                .HasForeignKey(d => d.pro_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_opr_pro_id");
        });

        modelBuilder.Entity<tb_parametro>(entity =>
        {
            entity.HasKey(e => e.par_id).HasName("PRIMARY");

            entity.Property(e => e.par_codigo).HasMaxLength(45);
            entity.Property(e => e.par_descripcion).HasMaxLength(100);
            entity.Property(e => e.par_valor).HasMaxLength(200);
        });

        modelBuilder.Entity<tb_password>(entity =>
        {
            entity.HasKey(e => e.pwd_id).HasName("PRIMARY");

            entity.HasIndex(e => e.usu_id, "fk_pwd_usu_id_idx");

            entity.Property(e => e.pwd_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.pwd_valor).HasMaxLength(300);

            entity.HasOne(d => d.usu).WithMany(p => p.tb_password)
                .HasForeignKey(d => d.usu_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pwd_usu_id");
        });

        modelBuilder.Entity<tb_producto>(entity =>
        {
            entity.HasKey(e => e.pro_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ent_id, "fk_pro_ent_id_idx");

            entity.HasIndex(e => e.est_id, "fk_pro_est_id_idx");

            entity.HasIndex(e => e.grp_id, "fk_pro_grp_id_idx");

            entity.HasIndex(e => e.tip_id_categoria, "fk_pro_tip_id_categoria_idx");

            entity.HasIndex(e => e.tip_id_color, "fk_pro_tip_id_color_idx");

            entity.HasIndex(e => e.tip_id_marca, "fk_pro_tip_id_marca_idx");

            entity.HasIndex(e => e.tip_id_talla, "fk_pro_tip_id_talla_idx");

            entity.Property(e => e.pro_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.pro_nombre).HasMaxLength(45);
            entity.Property(e => e.pro_precio).HasPrecision(12);

            entity.HasOne(d => d.ent).WithMany(p => p.tb_producto)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_ent_id");

            entity.HasOne(d => d.est).WithMany(p => p.tb_producto)
                .HasForeignKey(d => d.est_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_est_id");

            entity.HasOne(d => d.grp).WithMany(p => p.tb_producto)
                .HasForeignKey(d => d.grp_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_grp_id");

            entity.HasOne(d => d.tip_id_categoriaNavigation).WithMany(p => p.tb_productotip_id_categoriaNavigation)
                .HasForeignKey(d => d.tip_id_categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_tip_id_categoria");

            entity.HasOne(d => d.tip_id_colorNavigation).WithMany(p => p.tb_productotip_id_colorNavigation)
                .HasForeignKey(d => d.tip_id_color)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_tip_id_color");

            entity.HasOne(d => d.tip_id_marcaNavigation).WithMany(p => p.tb_productotip_id_marcaNavigation)
                .HasForeignKey(d => d.tip_id_marca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_tip_id_marca");

            entity.HasOne(d => d.tip_id_tallaNavigation).WithMany(p => p.tb_productotip_id_tallaNavigation)
                .HasForeignKey(d => d.tip_id_talla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pro_tip_id_talla");
        });

        modelBuilder.Entity<tb_producto_documento>(entity =>
        {
            entity.HasKey(e => e.pdo_id).HasName("PRIMARY");

            entity.HasIndex(e => e.doc_id, "fk_pdo_doc_id_idx");

            entity.HasIndex(e => e.pro_id, "fk_pdo_pro_id_idx");

            entity.HasOne(d => d.doc).WithMany(p => p.tb_producto_documento)
                .HasForeignKey(d => d.doc_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pdo_doc_id");

            entity.HasOne(d => d.pro).WithMany(p => p.tb_producto_documento)
                .HasForeignKey(d => d.pro_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pdo_pro_id");
        });

        modelBuilder.Entity<tb_provincia>(entity =>
        {
            entity.HasKey(e => e.prv_id).HasName("PRIMARY");

            entity.HasIndex(e => e.reg_id, "fk_prv_reg_id _idx");

            entity.Property(e => e.prv_nombre).HasMaxLength(45);

            entity.HasOne(d => d.reg).WithMany(p => p.tb_provincia)
                .HasForeignKey(d => d.reg_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_prv_reg_id ");
        });

        modelBuilder.Entity<tb_region>(entity =>
        {
            entity.HasKey(e => e.reg_id).HasName("PRIMARY");

            entity.Property(e => e.reg_nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<tb_rol>(entity =>
        {
            entity.HasKey(e => e.rol_id).HasName("PRIMARY");

            entity.Property(e => e.rol_nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<tb_rol_funcionalidad>(entity =>
        {
            entity.HasKey(e => e.rfu_id).HasName("PRIMARY");

            entity.HasIndex(e => e.fun_id, "tb_rol_funcionalidad_tb_funcionalidad_FK");

            entity.HasIndex(e => e.rol_id, "tb_rol_funcionalidad_tb_rol_FK");

            entity.Property(e => e.rfu_fecha_creacion).HasColumnType("datetime");

            entity.HasOne(d => d.fun).WithMany(p => p.tb_rol_funcionalidad)
                .HasForeignKey(d => d.fun_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_rol_funcionalidad_tb_funcionalidad_FK");

            entity.HasOne(d => d.rol).WithMany(p => p.tb_rol_funcionalidad)
                .HasForeignKey(d => d.rol_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_rol_funcionalidad_tb_rol_FK");
        });

        modelBuilder.Entity<tb_tipo>(entity =>
        {
            entity.HasKey(e => e.tip_id).HasName("PRIMARY");

            entity.HasIndex(e => e.cti_id, "fk_tip_cti_id_idx");

            entity.HasIndex(e => e.ent_id, "fk_tip_ent_id_idx");

            entity.Property(e => e.tip_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.tip_nombre).HasMaxLength(45);

            entity.HasOne(d => d.cti).WithMany(p => p.tb_tipo)
                .HasForeignKey(d => d.cti_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tip_cti_id");

            entity.HasOne(d => d.ent).WithMany(p => p.tb_tipo)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tip_ent_id");
        });

        modelBuilder.Entity<tb_tipo_documento>(entity =>
        {
            entity.HasKey(e => e.tdo_id).HasName("PRIMARY");

            entity.Property(e => e.tdo_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.tdo_nombre).HasMaxLength(45);
            entity.Property(e => e.tdo_path).HasMaxLength(200);
        });

        modelBuilder.Entity<tb_usuario>(entity =>
        {
            entity.HasKey(e => e.usu_id).HasName("PRIMARY");

            entity.HasIndex(e => e.ent_id, "fk_usu_ent_id_idx");

            entity.HasIndex(e => e.est_id, "fk_usu_est_id_idx");

            entity.HasIndex(e => e.rol_id, "fk_usu_rol_id_idx");

            entity.Property(e => e.usu_email).HasMaxLength(45);
            entity.Property(e => e.usu_fecha_creacion).HasColumnType("datetime");
            entity.Property(e => e.usu_nombre).HasMaxLength(50);

            entity.HasOne(d => d.ent).WithMany(p => p.tb_usuario)
                .HasForeignKey(d => d.ent_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usu_ent_id");

            entity.HasOne(d => d.est).WithMany(p => p.tb_usuario)
                .HasForeignKey(d => d.est_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usu_est_id");

            entity.HasOne(d => d.rol).WithMany(p => p.tb_usuario)
                .HasForeignKey(d => d.rol_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usu_rol_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
