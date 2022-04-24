using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Models;
using System.ComponentModel;

namespace ProjectPrototype.ViewModels
{
    public class GameMatch
    {

        public Game Game { get; set; }
        public Match MatchW { get; set; }

        public Team TeamW { get; set; }
        public Match MatchL { get; set; }
        public Team TeamL { get; set; }

        public GameMatch()
        {

        }
    }

    public class GameMatchStats : GameMatch
    {
        [DisplayName("Select Team")]
        public int? SelectedTeamId { get; set; }
        [DisplayName("Select Player")]
        public int? SelectedPlayerId { get; set; }

        public Player SelectedPlayer { get; set; }

        public Player DummyPlayer { get; set; }
        public GameMatchStats()
        {

        }
    }
}
