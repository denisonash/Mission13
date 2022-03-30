using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mission13.Models;

namespace Mission13.Components
{
    public class TeamViewComponent :ViewComponent
    {
        private IBowlersRepository _repo { get; set; }

        public TeamViewComponent(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            //Get all the teams and send the selected team name to the route
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            var teams = _repo.Teams.Select(t => t.TeamName)
                .Distinct()
                .OrderBy(t => t);

            return View(teams);
        }
    }
}
