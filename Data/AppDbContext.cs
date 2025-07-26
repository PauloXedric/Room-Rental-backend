using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RRMS.Entities;
using RRMS.Models.Identity;

namespace RRMS.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<EmergencyContactEntity> EmergencyContacts { get; set; }
        public DbSet<RoomEntity> Room { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(au => au.Status)
                .HasConversion<string>();

            modelBuilder.Entity<RoomEntity>()
               .Property(au => au.RoomType)
               .HasConversion<string>();

            modelBuilder.Entity<RoomEntity>()
               .Property(au => au.GenderRestriction)
               .HasConversion<string>();
        }

    }
}
