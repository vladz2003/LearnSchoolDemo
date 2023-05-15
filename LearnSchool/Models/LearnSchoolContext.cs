using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LearnSchool;

public partial class LearnSchoolContext : DbContext
{
    private static LearnSchoolContext _context;

    public static LearnSchoolContext GetContext()
    {
        if (_context == null)
            _context = new LearnSchoolContext();
        return _context;
    }
    public LearnSchoolContext()
    {
    }

    public LearnSchoolContext(DbContextOptions<LearnSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientService> ClientServices { get; set; }

    public virtual DbSet<ClientTag> ClientTags { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }

    public virtual DbSet<EmployeeService> EmployeeServices { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductHistory> ProductHistories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductManufacturer> ProductManufacturers { get; set; }

    public virtual DbSet<Productsparameter> Productsparameters { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceImage> ServiceImages { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LearnSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");
            entity.Property(e => e.ClientId)
            .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.TagId).HasColumnName("TagID");

            entity.HasOne(d => d.Gender).WithMany(p => p.Clients)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_Gender1");

            entity.HasOne(d => d.Tag).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_ClientTags");
        });

        modelBuilder.Entity<ClientService>(entity =>
        {
            entity.ToTable("ClientService");

            entity.Property(e => e.ClientServiceId).HasColumnName("ClientServiceID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DateOfService).HasColumnType("date");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientService_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientService_Service");
        });

        modelBuilder.Entity<ClientTag>(entity =>
        {
            entity.Property(e => e.ClientTagId).HasColumnName("ClientTagID");
            entity.Property(e => e.TagHashColor).HasMaxLength(50);
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeCategoryId).HasColumnName("EmployeeCategoryID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
            entity.Property(e => e.PassportSeries).HasMaxLength(50);
            entity.Property(e => e.PaymentRatio).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_EmployeeCategory");

            entity.HasOne(d => d.Gender).WithMany(p => p.Employees)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Gender");
        });

        modelBuilder.Entity<EmployeeCategory>(entity =>
        {
            entity.ToTable("EmployeeCategory");

            entity.Property(e => e.EmployeeCategoryId).HasColumnName("EmployeeCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CategoryPayment).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<EmployeeService>(entity =>
        {
            entity.ToTable("EmployeeService");

            entity.Property(e => e.EmployeeServiceId).HasColumnName("EmployeeServiceID");
            entity.Property(e => e.EmployeeCategoryId).HasColumnName("EmployeeCategoryID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.EmployeeServices)
                .HasForeignKey(d => d.EmployeeCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeService_EmployeeCategory");

            entity.HasOne(d => d.Service).WithMany(p => p.EmployeeServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeService_Service");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.ToTable("Parameter");

            entity.Property(e => e.ParameterId).HasColumnName("ParameterID");
            entity.Property(e => e.ParameterName).HasMaxLength(50);
            entity.Property(e => e.UnitTypeId).HasColumnName("UnitTypeID");

            entity.HasOne(d => d.UnitType).WithMany(p => p.Parameters)
                .HasForeignKey(d => d.UnitTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parameter_UnitType");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductCost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductManufacturerId).HasColumnName("ProductManufacturerID");
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");

            entity.HasOne(d => d.ProductManufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductManufacturer");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductHistory>(entity =>
        {
            entity.ToTable("ProductHistory");

            entity.Property(e => e.ProductHistoryId).HasColumnName("ProductHistoryID");
            entity.Property(e => e.BuyDate).HasColumnType("date");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductHistory_Product");

            entity.HasOne(d => d.Service).WithMany(p => p.ProductHistories)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductHistory_Service");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageId).HasColumnName("ProductImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<ProductManufacturer>(entity =>
        {
            entity.ToTable("ProductManufacturer");

            entity.Property(e => e.ProductManufacturerId).HasColumnName("ProductManufacturerID");
            entity.Property(e => e.DateOfBegging).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Productsparameter>(entity =>
        {
            entity.HasKey(e => e.ProductParameterId);

            entity.ToTable("Productsparameter");

            entity.Property(e => e.ProductParameterId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductParameterID");
            entity.Property(e => e.ParameterId).HasColumnName("ParameterID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Value).HasMaxLength(50);

            entity.HasOne(d => d.Parameter).WithMany(p => p.Productsparameters)
                .HasForeignKey(d => d.ParameterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productsparameter_Parameter");

            entity.HasOne(d => d.ProductParameter).WithOne(p => p.Productsparameter)
                .HasForeignKey<Productsparameter>(d => d.ProductParameterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productsparameter_Product");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceCost).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<ServiceImage>(entity =>
        {
            entity.ToTable("ServiceImage");

            entity.Property(e => e.ServiceImageId).HasColumnName("ServiceImageID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceImages)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceImage_Service");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.ToTable("UnitType");

            entity.Property(e => e.UnitTypeId).HasColumnName("UnitTypeID");
            entity.Property(e => e.UnitTypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
