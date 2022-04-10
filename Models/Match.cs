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
        [Required(ErrorMessage = "Please enter a home team.")]
        public int HomeTeamId { get; set; }
        [Required(ErrorMessage = "Please enter an away team.")]
        public int AwayTeamId { get; set; }
    }
}
