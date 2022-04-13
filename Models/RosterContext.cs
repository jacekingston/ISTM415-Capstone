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
                },
                new Team
                {
                    TeamId = 3,
                    TeamName = "Bad News Bears",
                    Mascot = "Brett the Bear",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 4,
                    TeamName = "The Hitmen",
                    Mascot = "Keeth",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 5,
                    TeamName = "Bisons",
                    Mascot = "Beevo",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 6,
                    TeamName = "Bat Attitudes",
                    Mascot = "Batkid",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 7,
                    TeamName = "Sliders",
                    Mascot = "McLovin",
                    Wins = 0,
                    Losses = 0
                },
                new Team
                {
                    TeamId = 8,
                    TeamName = "Lightning",
                    Mascot = "Mr. Electro",
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
                },
                new Game
                {
                    GameId = 2,
                    DateTime = new DateTime(2022, 4, 10, 2, 30, 0),
                    Location = "College Station, TX"
                },
                new Game
                {
                    GameId = 3,
                    DateTime = new DateTime(2022, 4, 10, 2, 30, 0),
                    Location = "College Station, TX"
                },
                new Game
                {
                    GameId = 4,
                    DateTime = new DateTime(2022, 4, 10, 2, 30, 0),
                    Location = "College Station, TX"
                }
            );

            // Match (Each game needs 2 matches)
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
                },
                new Match
                {
                    MatchId = 3,
                    GameId = 2,
                    TeamId = 3,
                    Score = 3,
                    Outcome = Outcome.Loss
                }
                ,
                new Match
                {
                    MatchId = 4,
                    GameId = 2,
                    TeamId = 4,
                    Score = 5,
                    Outcome = Outcome.Win
                }
                ,
                new Match
                {
                    MatchId = 5,
                    GameId = 3,
                    TeamId = 5,
                    Score = 6,
                    Outcome = Outcome.Tie
                }
                ,
                new Match
                {
                    MatchId = 6,
                    GameId = 3,
                    TeamId = 6,
                    Score = 6,
                    Outcome = Outcome.Tie
                },
                new Match
                {
                    MatchId = 7,
                    GameId = 4,
                    TeamId = 7,
                    Score = 13,
                    Outcome = Outcome.Win
                },
                new Match
                {
                    MatchId = 8,
                    GameId = 4,
                    TeamId = 8,
                    Score = 10,
                    Outcome = Outcome.Loss
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
                },
                new Manager
                {
                    ManagerId = 3,
                    FirstName = "Ryan",
                    LastName = "Walker",
                    Phone = 9725478392,
                    Email = "DubWalker@gmail.com",
                    TeamId = 3
                },
                new Manager
                {
                    ManagerId = 4,
                    FirstName = "Joe",
                    LastName = "Burrow",
                    Phone = 9725478392,
                    Email = "goated@gmail.com",
                    TeamId = 4
                },
                new Manager
                {
                    ManagerId = 5,
                    FirstName = "Tom",
                    LastName = "Brady",
                    Phone = 9725478392,
                    Email = "Deflate@gmail.com",
                    TeamId = 5
                },
                new Manager
                {
                    ManagerId = 6,
                    FirstName = "Brett",
                    LastName = "Favre",
                    Phone = 9725478392,
                    Email = "OldBrett@gmail.com",
                    TeamId = 6
                },
                new Manager
                {
                    ManagerId = 7,
                    FirstName = "Trey",
                    LastName = "Kingston",
                    Phone = 9725478392,
                    Email = "TreyK@gmail.com",
                    TeamId = 7
                },
                new Manager
                {
                    ManagerId = 8,
                    FirstName = "Kade",
                    LastName = "Kingston",
                    Phone = 9725478392,
                    Email = "KadeK@gmail.com",
                    TeamId = 8
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
                },
                new Player
                {
                    PlayerId = 3,
                    TeamId = 3,
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
                },
                new Player
                {
                    PlayerId = 4,
                    TeamId = 4,
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
                },
                new Player
                {
                    PlayerId = 5,
                    TeamId = 5,
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
                },
                new Player
                {
                    PlayerId = 6,
                    TeamId = 6,
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
                },
                new Player
                {
                    PlayerId = 7,
                    TeamId = 7,
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
                },
                new Player
                {
                    PlayerId = 8,
                    TeamId = 8,
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
