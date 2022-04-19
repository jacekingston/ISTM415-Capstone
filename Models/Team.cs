using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrototype.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Please enter a team name.")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Please enter a team mascot.")]
        public string Mascot { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public IEnumerable<Match> Matches { get; set; }

        public IEnumerable<Player> Players { get; set; }

        public IEnumerable<Manager> Managers { get; set; }
    }
}
