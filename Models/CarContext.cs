using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya_BD.Models
{
    public class CarContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<Tovar> Tovars { get; set; }
        public DbSet<Korzina> Korzinas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }


        public CarContext(DbContextOptions<CarContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
