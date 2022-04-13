using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrototype.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public int Score { get; set; }

        public Outcome Outcome { get; set; }

        public Game Game { get; set; }
        public Team Team { get; set; }
    }

    public enum Outcome
    {
        Win,
        Loss,
        Tie
    }
}
