using LoginFormAspCore.Models;
using Microsoft.EntityFrameworkCore;

public partial class LoginFormContext : DbContext
{
    public LoginFormContext()
    {
    }

    public LoginFormContext(DbContextOptions<LoginFormContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Add your database configuration here, if necessary.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>(entity =>
        {
            // Set the Id property as the primary key
            entity.HasKey(e => e.Id);

            // Configure the table name
            entity.ToTable("User_Info");

            // Configure the Id property to be an identity column (auto-generated)
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Indicates that the value will be auto-generated

            // Configure other properties
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
