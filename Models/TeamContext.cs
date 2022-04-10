using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options) { }
        public DbSet<Team> Teams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    TeamId = 1,
                    TeamName = "Roughriders",
                    Mascot = "Horse",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 2,
                    TeamName = "Sharks",
                    Mascot = "Hammerhead",
                    Wins = 0,
                    Losses = 0
                }
            );
        }
    }
}
