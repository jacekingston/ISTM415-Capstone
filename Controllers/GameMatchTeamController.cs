using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Controllers;
using ProjectPrototype.ViewModels;
using ProjectPrototype.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Controllers
{
    public class GameMatchTeamController : Controller
    {

        private readonly RosterContext _context;

        public GameMatchTeamController(RosterContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            /*var result =  await _context.Matches
                .GroupBy(m => m.GameId)
                .Select(m => new { m.Key, m2 = m})
                .ToDictionaryAsync(m => m.Key, m => m.m2);*/

            // Manual groupings cause LINQ dumb
            Dictionary<int, List<Match>> groups = new Dictionary<int, List<Match>>();
            foreach (var match in _context.Matches)
            {
                // Hack to fill in object references
                match.Game = _context.Games.Find(match.GameId);
                match.Team = _context.Teams.Find(match.TeamId);

                if (!groups.ContainsKey(match.GameId))
                    groups.Add(match.GameId, new List<Match>());
                groups[match.GameId].Add(match);

                
            }

            List<GameMatchTeam> test = new List<GameMatchTeam>();
            // Iterate over match groupings
            foreach (var groupings in groups)
            {
            // Check if grouping has 2 matches (it should always)
                Match a = groupings.Value.ElementAt(0);
                Match b = groupings.Value.ElementAt(1);
                if(a.Score > b.Score)
                {
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = a,
                        MatchLinkL = b
                    });
                    // A is winner
                }else if(a.Score < b.Score)
                {
                    // B is Winner
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = b,
                        MatchLinkL = a
                    });
                }
                else if(a.Score == b.Score)
                {
                    // Tie!
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = a,
                        MatchLinkL = b
                    });
                }
            }
            return View(test);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SearchString)
        {
            if (SearchString == null)
                SearchString = "";

            // Manual groupings cause LINQ dumb
            Dictionary<int, List<Match>> groups = new Dictionary<int, List<Match>>();
            foreach (var match in _context.Matches)
            {

                match.Game = _context.Games.Find(match.GameId);
                match.Team = _context.Teams.Find(match.TeamId);

                if (!match.Team.TeamName.Contains(SearchString))
                    continue;

                if (!groups.ContainsKey(match.GameId))
                {
                    groups.Add(match.GameId, new List<Match>());
                }
                // group made
                groups[match.GameId].Add(match);
                
            }

            // Second pass to add non search pair
            foreach(var match in _context.Matches)
            {
                if(groups.ContainsKey(match.GameId) && groups[match.GameId].Count < 2)
                {
                    // group made, and only 1 element
                    // Add other
                    groups[match.GameId].Add(match);
                }
            }

            List<GameMatchTeam> test = new List<GameMatchTeam>();
            // Iterate over match groupings
            foreach (var groupings in groups)
            {
                // Check if grouping has 2 matches (it should always)
                Match a = groupings.Value.ElementAt(0);
                Match b = groupings.Value.ElementAt(1);
                if (a.Score > b.Score)
                {
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = a,
                        MatchLinkL = b
                    });
                    // A is winner
                }
                else if (a.Score < b.Score)
                {
                    // B is Winner
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = b,
                        MatchLinkL = a
                    });
                }
                else if (a.Score == b.Score)
                {
                    // Tie!
                    test.Add(new GameMatchTeam
                    {
                        MatchLinkW = a,
                        MatchLinkL = b
                    });
                }
            }
            return View(test);
        }

    }


}
