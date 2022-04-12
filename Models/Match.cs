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

        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Team> teams { get; set; }
    }
}
