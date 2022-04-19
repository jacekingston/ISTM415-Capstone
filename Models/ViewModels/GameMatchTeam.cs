using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Models;

namespace ProjectPrototype.ViewModels
{
    public class GameMatchTeam
    {
        /*public IEnumerable<Game> GameLink { get; set; }
        public IEnumerable<Match> MatchLinkW { get; set; }
        public IEnumerable<Team> TeamLinkW { get; set; }*/

        public Match MatchLinkW { get; set; }
        public Match MatchLinkL { get; set; }

        public GameMatchTeam()
        {

        }
    }
}
