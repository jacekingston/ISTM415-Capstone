using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class RosterContext : DbContext
    {
        public RosterContext(DbContextOptions<RosterContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var MatchBuilder = modelBuilder.Entity<Match>().ToTable(nameof(Match));
                MatchBuilder.HasOne(c => c.Game).WithMany(g => g.Matches);
                MatchBuilder.HasOne(c => c.Team).WithMany(t => t.Matches);
            var teamBuilder = modelBuilder.Entity<Team>().ToTable(nameof(Team));
                teamBuilder.HasMany(t => t.Players).WithOne(p => p.Team);
                teamBuilder.HasMany(t => t.Managers).WithOne(ma => ma.Team);
            modelBuilder.Entity<Game>().ToTable(nameof(Game));
            modelBuilder.Entity<Player>().ToTable(nameof(Player));
            modelBuilder.Entity<Manager>().ToTable(nameof(Manager));

            PopulateDatabase(modelBuilder);
        }

        protected void PopulateDatabase(ModelBuilder modelBuilder)
        {
            // Team
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

            // Game
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    GameId = 1,
                    DateTime = new DateTime(2022, 4, 10, 2, 30, 0),
                    Location = "College Station, TX"
                }
            );

            // Match
            modelBuilder.Entity<Match>().HasData(
                new Match
                {
                    MatchId = 1,
                    GameId = 1,
                    TeamId = 1,
                    Score = 2,
                    Outcome = Outcome.Loss
                },
                new Match
                {
                    MatchId = 2,
                    GameId = 1,
                    TeamId = 2,
                    Score = 12,
                    Outcome = Outcome.Win
                }
            );

            // Manager
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

            // Player
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

        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }


    }
}
