using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    PlayerId = 1,
                    TeamId = 1,
                    FirstName = "Max",
                    LastName = "Smith",
                    DOB = new DateTime(2013, 4, 12),
                    Height = 49,
                    Weight = 65,
                    NumAtBats = 0,
                    NumHits = 0,
                    NumHittingStrikeouts = 0,
                    NumHomeruns = 0,
                    NumRBI = 0,
                    NumWalks = 0,
                    Position = "C",
                    NumPlays = 0,
                    NumErrors = 0,
                    NumInningsPitched = 0,
                    NumEarnedRunsAllowed = 0,
                    NumWalksAllowed = 0,
                    NumPitchingStrikeouts = 0,
                    NumHomerunsAllowed = 0
                },
                new Player
                {
                    PlayerId = 2,
                    TeamId = 2,
                    FirstName = "Jackson",
                    LastName = "Frome",
                    DOB = new DateTime(2013, 2, 5),
                    Height = 57,
                    Weight = 91,
                    NumAtBats = 0,
                    NumHits = 0,
                    NumHittingStrikeouts = 0,
                    NumHomeruns = 0,
                    NumRBI = 0,
                    NumWalks = 0,
                    Position = "RF",
                    NumPlays = 0,
                    NumErrors = 0,
                    NumInningsPitched = 0,
                    NumEarnedRunsAllowed = 0,
                    NumWalksAllowed = 0,
                    NumPitchingStrikeouts = 0,
                    NumHomerunsAllowed = 0
                }
            );
        }
    }
}
