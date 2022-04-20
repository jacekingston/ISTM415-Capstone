using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Controllers;
using ProjectPrototype.ViewModels;
using ProjectPrototype.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectPrototype.Controllers
{
    public class GameMatchController : Controller
    {

        private readonly RosterContext _context;

        public GameMatchController(RosterContext context)
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
                _context.Matches.Update(match);
                

                if (!groups.ContainsKey(match.GameId))
                    groups.Add(match.GameId, new List<Match>());
                groups[match.GameId].Add(match);

            }

            await _context.SaveChangesAsync();

            List<GameMatch> fullGames = new List<GameMatch>();
            // Iterate over match groupings
            foreach (var groupings in groups)
            {
            // Check if grouping has 2 matches (it should always)
                Match a = groupings.Value.ElementAt(0);
                Match b = groupings.Value.ElementAt(1);
                var fullGame = DetermineGameWinner(a, b);
                if (fullGame != null)
                    fullGames.Add(fullGame);
            }
            return View(fullGames);
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

                if (!Util.UseSearchTerm(SearchString, match.Team.TeamName))
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
                if(groups.ContainsKey(match.GameId) && groups[match.GameId].Count < 2 && groups[match.GameId].ElementAt(0).TeamId != match.TeamId)
                {
                    // group made, and only 1 element
                    // Add other
                    groups[match.GameId].Add(match);
                }
            }

            List<GameMatch> fullGames = new List<GameMatch>();
            // Iterate over match groupings
            foreach (var groupings in groups)
            {
                // Check if grouping has 2 matches (it should always)
                Match a = groupings.Value.ElementAt(0);
                Match b = groupings.Value.ElementAt(1);
                var fullGame = DetermineGameWinner(a, b);
                if (fullGame != null)
                    fullGames.Add(fullGame);
            }
            return View(fullGames);
        }

        private GameMatch DetermineGameWinner(Match a, Match b)
        {
            if (a.Score > b.Score)
            {
                return new GameMatch
                {
                    MatchLinkW = a,
                    MatchLinkL = b
                };
                // A is winner
            }
            else if (a.Score < b.Score)
            {
                // B is Winner
                return new GameMatch
                {
                    MatchLinkW = b,
                    MatchLinkL = a
                };
            }
            else if (a.Score == b.Score)
            {
                // Tie!
                return new GameMatch
                {
                    MatchLinkW = a,
                    MatchLinkL = b
                };
            }
            return null;
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName");
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public async Task<IActionResult> Create(GameMatch fullGame)
        {
            if (ModelState.IsValid)
            {
                Match a = fullGame.MatchLinkW;
                Match b = fullGame.MatchLinkL;
                // Add Game
                var trackGame = _context.Add(new Game
                {
                    DateTime = a.Game.DateTime,
                    Location = a.Game.Location
                });
                // Save so auto ID is generated
                await _context.SaveChangesAsync();

                // Add Matches
                _context.Add(new Match
                {
                    GameId = trackGame.Entity.GameId,
                    TeamId = a.TeamId,
                    Score = a.Score
                });
                _context.Add(new Match
                {
                    GameId = trackGame.Entity.GameId,
                    TeamId = b.TeamId,
                    Score = b.Score
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchLinkW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchLinkL.TeamId);
            return View(fullGame);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            var matches = _context.Matches.Where(m => m.GameId == game.GameId).ToList();
            if(matches.Count() != 2)
            {
                return NotFound();
            }

            var a = matches.ElementAt(0);
            var b = matches.ElementAt(1);
            var gameMatch = DetermineGameWinner(a, b);

            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", gameMatch.MatchLinkW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", gameMatch.MatchLinkL.TeamId);
            return View(gameMatch);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GameMatch fullGame)
        {
            if (id != fullGame.MatchLinkW.Game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fullGame.MatchLinkL.Game = fullGame.MatchLinkW.Game;
                    // Add Game
                    _context.Update(fullGame.MatchLinkW.Game);
                    //_context.Update(fullGame.MatchLinkL.Game);
                    // Save so auto ID is generated
                    await _context.SaveChangesAsync();

                    // Add Matches
                    _context.Update(fullGame.MatchLinkW);
                    _context.Update(fullGame.MatchLinkL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Games.Any(g => g.GameId == fullGame.MatchLinkW.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchLinkW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchLinkL.TeamId);
            return View(fullGame);
        }

        public async Task<IActionResult> Stats(int? id)
        {
            #region Build GameMatch

            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            var matches = _context.Matches.Where(m => m.GameId == game.GameId).ToList();
            if (matches.Count() != 2)
            {
                return NotFound();
            }

            var a = matches.ElementAt(0);
            var b = matches.ElementAt(1);
            var gameMatch = DetermineGameWinner(a, b);

            return View(gameMatch);
            #endregion
        }

        // POST: Matches/Edit/5
        [HttpPost]
        public async Task<IActionResult> Stats(int id, GameMatch fullGame)
        {
            return View(fullGame);
        }
    }
}
