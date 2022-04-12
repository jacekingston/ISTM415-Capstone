using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options) {
            Database.EnsureCreated();
            Database.Migrate(); }

        public MatchContext() { }
        public DbSet<Match> Matches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasData(
                new Match
                {
                    MatchId = 1,
                    GameId = 1,
                    TeamId = 1,
                    Score = 2
                },
                new Match
                {
                    MatchId = 2,
                    GameId = 1,
                    TeamId = 2,
                    Score = 12
                }
            );
        }
    }
}
