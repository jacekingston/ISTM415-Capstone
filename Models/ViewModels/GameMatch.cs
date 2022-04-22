using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Models;

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
        public IEnumerable<Player> PlayersW { get; set; }

        public IEnumerable<Player> PlayersL { get; set; }

        public GameMatchStats()
        {

        }
    }
}
