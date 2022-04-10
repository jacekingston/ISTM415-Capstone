using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class ManagerContext : DbContext
    {
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }
        public DbSet<Manager> Managers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    ManagerId = 1,
                    FirstName = "Mike",
                    LastName = "Stevens",
                    Phone = 9723389204,
                    Email = "mstevens@verizon.net",
                    TeamId = 1
                },
                new Manager
                {
                    ManagerId = 2,
                    FirstName = "John",
                    LastName = "Freeman",
                    Phone = 9725478392,
                    Email = "freemansports@gmail.com",
                    TeamId = 2
                }
            );
        }
    }
}
