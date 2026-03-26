using Abp.Zero.EntityFrameworkCore;
using Pharmacy.Authorization.Roles;
using Pharmacy.Authorization.Users;
using Pharmacy.EntityFrameworkCore.Configurations;
using Pharmacy.MultiTenancy;
using Pharmacy.Products;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.EntityFrameworkCore;

public class PharmacyDbContext : AbpZeroDbContext<Tenant, Role, User, PharmacyDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Product> Products { get; set; }

    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyCustomConfiguration();
    }
}
