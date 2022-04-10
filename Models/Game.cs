using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrototype.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required(ErrorMessage = "Please enter a date-and-time.")]
        public DateTime DateTime { get; set; }
        [Required(ErrorMessage = "Please enter the winner's score.")]
        public int WinnerScore { get; set; }
        [Required(ErrorMessage = "Please enter the loser's score.")]
        public int LoserScore { get; set; }
        [Required(ErrorMessage = "Please enter a location.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter a winner.")]
        public int WinnerTeamId { get; set; }
        [Required(ErrorMessage = "Please enter a loser.")]
        public int LoserTeamId { get; set; }
    }
}
