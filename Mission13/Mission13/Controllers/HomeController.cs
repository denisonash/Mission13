using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        //public IActionResult Index(int teamId = 0)
        //{
        //    //var b = _repo.Bowlers.ToList();
        //    //Include actually does the join
        //    // .Include(x => "Teams")
        //    var b = _repo.Bowlers
        //            .Where(b => b.TeamID == teamId || teamId == 0)
        //            .OrderBy(b => b.BowlerLastName).ToList();
        //    return View(b);
        //}
        public IActionResult Index(string teamName)
        {
            //var b = _repo.Bowlers.ToList();
            //Include actually does the join
            // .Include(x => "Teams")
            var b = _repo.Bowlers.Include(b => b.Team)
                    .Where(b => b.Team.TeamName == teamName || teamName == null)
                    .OrderBy(b => b.BowlerLastName).ToList();
            return View(b);
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _repo.Teams.ToList();

            var b = _repo.Bowlers.Single(b => b.BowlerID == bowlerid);

            return View("BowlerInfo", b);
        }

        [HttpPost]
        public IActionResult Edit(Bowler up)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Teams = _repo.Teams.ToList();

                return View("BowlerInfo", up);
            }

            _repo.SaveBowler(up);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var b = _repo.Bowlers.Single(b => b.BowlerID == bowlerid);

            _repo.DeleteBowler(b);

            return RedirectToAction("Index");
            //return View(b);
        }

        //[HttpPost]
        //public IActionResult Delete(Bowler b)
        //{
        //    _repo.DeleteBowler(b);

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Teams = _repo.Teams.ToList();
            return View("BowlerInfo");
        }

        [HttpPost]
        public IActionResult Create(Bowler b)
        {
            // To check validation
            if (ModelState.IsValid)
            {
                _repo.CreateBowler(b);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Majors = _repo.Teams.ToList();

                return View();
            }

        }
    }
}
