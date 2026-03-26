using Microsoft.EntityFrameworkCore;

namespace Pharmacy.EntityFrameworkCore.Configurations
{
    public static class PharmacyDbContextConfiguration
    {
        public static void ApplyCustomConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}