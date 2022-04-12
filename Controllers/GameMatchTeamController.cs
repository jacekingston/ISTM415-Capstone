using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrototype.Models;
using ProjectPrototype.ViewModels;

namespace ProjectPrototype.Controllers
{
    public class GameMatchTeamController : Controller
    {

        List<Game> Games = new List<Game>();
        List<Match> Matches = new List<Match>();
        List<Team> Teams = new List<Team>();

        public GameMatchTeamController()
        {
        }

        public IActionResult Index()
        {
            var coreModel = from m in Matches
                            join g in Games on m.GameId equals g.GameId into c
                            join t in Teams on m.TeamId equals t.TeamId
                            from g in c.DefaultIfEmpty()
                            select m;


            return View(coreModel);
        }
    }
}
