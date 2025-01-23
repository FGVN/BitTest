using Microsoft.EntityFrameworkCore;
using BitTest.Models;

namespace BitTest.Data;

public class CsvDbContext : DbContext
{
    public DbSet<CsvRecord> CsvRecords { get; set; }

    public CsvDbContext(DbContextOptions<CsvDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CsvRecord>(entity =>
        {
            entity.HasKey(e => e.Id); 
            entity.HasIndex(e => e.Phone).IsUnique(); 
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).IsRequired();
            entity.Property(e => e.Married).IsRequired();
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(15);
            entity.Property(e => e.Salary).IsRequired().HasColumnType("decimal(18,2)");
        });
    }
}
