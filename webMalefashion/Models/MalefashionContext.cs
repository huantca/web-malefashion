using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webMalefashion.Models;

public partial class MalefashionContext : DbContext
{
    public MalefashionContext()
    {
    }

    public MalefashionContext(DbContextOptions<MalefashionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SellReceipt> SellReceipts { get; set; }

    public virtual DbSet<SellReceiptDetail> SellReceiptDetails { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=malefashion.mssql.somee.com;Initial Catalog=malefashion;User Id=dec_31_SQLLogin_1; Password=gvstelp8my;Integrated Security=False;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3213E83F9333895A");

            entity.ToTable("category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F40D44A33");

            entity.ToTable("customer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("ntext")
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationTime)
                .HasColumnType("datetime")
                .HasColumnName("registration_time");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__manufact__3213E83F7992A1BB");

            entity.ToTable("manufacturer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ColorHex, e.SizeId }).HasName("PK__option__22A384FB5AA929C2");

            entity.ToTable("option");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ColorHex)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("color_hex");
            entity.Property(e => e.SizeId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("size_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.Product).WithMany(p => p.Options)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__option__product___1AD3FDA4");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83F321337E2");

            entity.ToTable("permission");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Position).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__permissio__posit__1CBC4616");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__position__3213E83FA6620B6A");

            entity.ToTable("position");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F1FDD523E");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("description");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__product__categor__19DFD96B");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK__product__manufac__1BC821DD");
        });

        modelBuilder.Entity<SellReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sell_rec__3213E83F57F5A236");

            entity.ToTable("sell_receipt");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Customer).WithMany(p => p.SellReceipts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__sell_rece__custo__1EA48E88");

            entity.HasOne(d => d.Staff).WithMany(p => p.SellReceipts)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__sell_rece__staff__2180FB33");
        });

        modelBuilder.Entity<SellReceiptDetail>(entity =>
        {
            entity.HasKey(e => new { e.SellReceiptId, e.ProductId }).HasName("PK__sell_rec__896E969B1E6A43CC");

            entity.ToTable("sell_receipt_details");

            entity.Property(e => e.SellReceiptId).HasColumnName("sell_receipt_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Discount).HasColumnName("discount");

            entity.HasOne(d => d.Product).WithMany(p => p.SellReceiptDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sell_rece__produ__208CD6FA");

            entity.HasOne(d => d.SellReceipt).WithMany(p => p.SellReceiptDetails)
                .HasForeignKey(d => d.SellReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sell_rece__sell___1F98B2C1");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__staff__3213E83FA6C1FFB9");

            entity.ToTable("staff");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("ntext")
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EndWorking)
                .HasColumnType("date")
                .HasColumnName("end_working");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.StartWorking)
                .HasColumnType("date")
                .HasColumnName("start_working");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__staff__position___1DB06A4F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
