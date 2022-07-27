using KlickitTask.Models;
using Microsoft.EntityFrameworkCore;

namespace KlickitTask.Data
{
    public class KlickitTaskEnteties: DbContext
    {
        public KlickitTaskEnteties():base() { }
        public KlickitTaskEnteties(DbContextOptions options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; } 
        public DbSet<Order> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasDiscriminator<UserType>("userRole")
                .HasValue<Admin>(UserType.Admin)
                .HasValue<Customer>(UserType.Customer);
        }


    }
}
