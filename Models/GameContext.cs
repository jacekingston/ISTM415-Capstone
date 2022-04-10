using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    GameId = 1,
                    DateTime = new DateTime(2022, 4, 10, 2, 30, 0),
                    WinnerScore = 4,
                    LoserScore = 1,
                    Location = "College Station, TX",
                    WinnerTeamId = 1,
                    LoserTeamId = 2
                }
            );
        }
    }
}
