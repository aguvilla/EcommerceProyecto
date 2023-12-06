using System;
using System.Collections.Generic;
using Ecommerce.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Web.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteMetodosPago> ClienteMetodosPagos { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<Direccion> Direcciones { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Orden> Ordenes { get; set; }

    public virtual DbSet<OrdenCarrito> OrdenesCarritos { get; set; }

    public virtual DbSet<OrdenPago> OrdenesPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<TipoDocumento> TiposDocumentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q7TDL4E\\MSSQLSERVER01;Initial Catalog=Ecommerce;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E554C9F1A2");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Eliminado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0876834CB9F");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Clientes__531402F3F7C04636").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_TiposDocumento");
        });

        modelBuilder.Entity<ClienteMetodosPago>(entity =>
        {
            entity.HasKey(e => e.MetodoPagoId).HasName("PK__ClienteM__A8FEAF545333E039");

            entity.ToTable("ClienteMetodosPago");

            entity.Property(e => e.Metodo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Proveedor)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClienteMetodosPagos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteMetodosPago_Clientes");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.DescuentoId).HasName("PK__Descuent__0C2C1118B2E9207C");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Eliminado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.DireccionId).HasName("PK__Direccio__68906D64AA2470D1");

            entity.Property(e => e.Barrio)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Partido)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direcciones_Clientes");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.LocalidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direcciones_Localidades");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.ProvinciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direcciones_Provincias");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE9106AEA2398");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Empleado__531402F3BF27ACFC").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cargo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Direccion).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.DireccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Direccion");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_TiposDocumento");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24D76B7D31DE");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Eliminado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.LocalidadId).HasName("PK__Localida__6E2890A2914BDFF8");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Provincia).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.ProvinciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Localidades_Provincias");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcaId).HasName("PK__Marcas__D5B1CD8B4F97FD55");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Eliminado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("PK__Ordenes__C088A504A4B6422F");

            entity.Property(e => e.Creacion).HasColumnType("datetime");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Modificacion).HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Clientes");
        });

        modelBuilder.Entity<OrdenCarrito>(entity =>
        {
            entity.HasKey(e => e.OrdenCarritoId).HasName("PK__OrdenesC__335BCFAAA9742FDF");

            entity.ToTable("OrdenesCarrito");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenesCarritos)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenesCarrito_Ordenes");

            entity.HasOne(d => d.Producto).WithMany(p => p.OrdenesCarritos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenesCarrito_Productos");
        });

        modelBuilder.Entity<OrdenPago>(entity =>
        {
            entity.HasKey(e => e.OrdenPagoId).HasName("PK__OrdenesP__6C33F866520AA250");

            entity.ToTable("OrdenesPago");

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Metodo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Modificacion).HasColumnType("datetime");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenesPagos)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenesPago_Ordenes");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3031B7634");

            entity.HasIndex(e => e.Nombre, "UQ__Producto__75E3EFCFDB6C078A").IsUnique();

            entity.Property(e => e.Creado).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Eliminado).HasColumnType("datetime");
            entity.Property(e => e.Modificado).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.SKU)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Productos)
                .HasForeignKey(d => d.DescuentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Descuentos");

            entity.HasOne(d => d.Inventario).WithMany(p => p.Productos)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Inventarios");

            entity.HasOne(d => d.Marca).WithMany(p => p.Productos)
                .HasForeignKey(d => d.MarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marcas");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.ProvinciaId).HasName("PK__Provinci__F7CBC77753C24CBC");

            entity.HasIndex(e => e.Descripcion, "UQ__Provinci__92C53B6CA874DACA").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumentoId).HasName("PK__TiposDoc__A329EA87A54E2C93");

            entity.ToTable("TiposDocumento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
