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
            GameMatch g = new GameMatch();
            if (a.Score >= b.Score)
            {
                g.MatchW = a;
                g.MatchL = b;
                // A is winner/Tie
            }
            else if (a.Score < b.Score)
            {
                // B is Winner
                g.MatchW = b;
                g.MatchL = a;
            }
            g.Game = a.Game;
            g.TeamW = _context.Teams.FirstOrDefault(t => t.TeamId == g.MatchW.TeamId);
            g.TeamL = _context.Teams.FirstOrDefault(t => t.TeamId == g.MatchL.TeamId);
            return g;
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
                // Add Game
                var trackGame = _context.Add(new Game
                {
                    DateTime = fullGame.Game.DateTime,
                    Location = fullGame.Game.Location
                });
                // Save so auto ID is generated
                await _context.SaveChangesAsync();

                // Add Matches
                _context.Add(new Match
                {
                    GameId = trackGame.Entity.GameId,
                    TeamId = fullGame.TeamW.TeamId,
                    Score = fullGame.MatchW.Score
                });
                _context.Add(new Match
                {
                    GameId = trackGame.Entity.GameId,
                    TeamId = fullGame.TeamL.TeamId,
                    Score = fullGame.MatchL.Score
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchL.TeamId);
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

            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", gameMatch.MatchW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", gameMatch.MatchL.TeamId);
            return View(gameMatch);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(GameMatch fullGame)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var matchW = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == fullGame.MatchW.MatchId);
                    var matchL = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == fullGame.MatchL.MatchId);

                    if (matchW != default(Match) && matchL != default(Match))
                    {

                        matchW.Score = fullGame.MatchW.Score;
                        matchW.TeamId = fullGame.TeamW.TeamId;
                        matchL.Score = fullGame.MatchL.Score;
                        matchL.TeamId = fullGame.TeamL.TeamId;
                        _context.UpdateRange(matchW, matchL);

                        var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == fullGame.Game.GameId);

                        if(game != default(Game))
                        {
                            game.DateTime = fullGame.Game.DateTime;
                            game.Location = fullGame.Game.Location;

                            _context.Update(game);

                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Games.Any(g => g.GameId == fullGame.Game.GameId))
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

            // ModelState invalid
            ViewData["TeamIdA"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchW.TeamId);
            ViewData["TeamIdB"] = new SelectList(_context.Teams, "TeamId", "TeamName", fullGame.MatchL.TeamId);
            return View(fullGame);
        }

        public async Task<IActionResult> Stats(int? id)
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
            if (matches.Count() != 2)
            {
                return NotFound();
            }

            var a = matches.ElementAt(0);
            var b = matches.ElementAt(1);
            var gameMatch = DetermineGameWinner(a, b);

            GameMatchStats g = new GameMatchStats
            {
                Game = gameMatch.Game,
                MatchW = gameMatch.MatchW,
                MatchL = gameMatch.MatchL,
                TeamW = gameMatch.TeamW,
                TeamL = gameMatch.TeamL
            };

            var players = _context.Players.Where(p => p.TeamId == g.TeamW.TeamId).OrderBy(p => p.LastName);

            var PlayersSelectable = players.Select(s => new
            {
                PlayerId = s.PlayerId,
                DisplayName = s.FirstName + ", " + s.LastName + ": " + s.Position
            }).ToList();

            ViewData["TeamSelect"] = new SelectList(new List<Team> {g.TeamW, g.TeamL }, "TeamId", "TeamName");
            ViewData["PlayerSelect"] = new SelectList(PlayersSelectable, "PlayerId", "DisplayName");

            return View(g);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        public async Task<IActionResult> Stats(GameMatchStats fullGame, string saveDummy = "")
        {
            bool applyDummy = saveDummy != "";
            if (ModelState.IsValid)
            {
                // Save Player Stats
                if (fullGame.SelectedPlayer != null && applyDummy)
                {
                    var savePlayer = _context.Players.FirstOrDefault(p => p.PlayerId == fullGame.SelectedPlayerId);
                    savePlayer.AddGameStats(fullGame.DummyPlayer);
                    _context.Update(savePlayer);
                    await _context.SaveChangesAsync();
                }

                // Match Stats
                var matches = _context.Matches.Where(m => m.GameId == fullGame.Game.GameId).ToList();
                if (matches.Count() != 2)
                {
                    return NotFound();
                }

                var a = matches.ElementAt(0);
                var b = matches.ElementAt(1);
                var gameMatch = DetermineGameWinner(a, b);
                fullGame.MatchW = gameMatch.MatchW;
                fullGame.MatchL = gameMatch.MatchL;

                // Prep selected player and dummy
                fullGame.SelectedPlayer = _context.Players.FirstOrDefault(p => p.PlayerId == fullGame.SelectedPlayerId);

                if (fullGame.DummyPlayer == null)
                    fullGame.DummyPlayer = new Player();

                //ModelState.Remove("GameMatch.DummyPlayer");
                ModelState.Clear();
                fullGame.DummyPlayer.SetAsDummy(fullGame.SelectedPlayer);

                // Filled Selected List based on team and player pick
                var players = _context.Players.Where(p => p.TeamId == fullGame.SelectedTeamId).OrderBy(p => p.LastName);

                var PlayersSelectable = players.Select(s => new
                {
                    PlayerId = s.PlayerId,
                    DisplayName = s.FirstName + ", " + s.LastName + ": " + s.Position
                }).ToList();

                var SelectableTeam = fullGame.TeamW.TeamId == fullGame.SelectedTeamId ? fullGame.TeamW : fullGame.TeamL;
                var SelectablePlayer = PlayersSelectable.FirstOrDefault(p => p.PlayerId == fullGame.SelectedPlayerId);

                ViewData["TeamSelect"] = new SelectList(new List<Team> { fullGame.TeamW, fullGame.TeamL }, "TeamId", "TeamName", SelectableTeam);
                ViewData["PlayerSelect"] = new SelectList(PlayersSelectable, "PlayerId", "DisplayName", SelectablePlayer);


            }
            return View(fullGame);
        }


        public async Task<IActionResult> SimulateSeason(int year)
        {
            var DBPopulator = new DBPopulator();
            DBPopulator.SimulateSeason(_context, year);
            return RedirectToAction(nameof(Index));
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            if (matches.Count() != 2)
            {
                return NotFound();
            }

            var a = matches.ElementAt(0);
            var b = matches.ElementAt(1);
            var gameMatch = DetermineGameWinner(a, b);

            return View(gameMatch);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

            _context.Games.Remove(game);
            _context.Matches.RemoveRange(a, b);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
