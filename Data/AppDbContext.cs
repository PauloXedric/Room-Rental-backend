using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RRMS.Entities;
using RRMS.Models.Identity;
using RRMS.Views;

namespace RRMS.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<EmergencyContactEntity> EmergencyContacts { get; set; }
        public DbSet<RoomEntity> Room { get; set; }
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }



        public DbSet<UserEmergencyContactView> UserEmergencyContact { get; set; }

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



             modelBuilder.Entity<UserEmergencyContactView>()
               .HasNoKey()
               .ToView("UserEmergencyContact");
        }

    }
}
