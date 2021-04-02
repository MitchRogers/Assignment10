using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            _context = con;
        }

        public IActionResult Index()
        {
            return View();
        }
        //pass in the team, teamName, and default the pageNum to be on home page (page 0)
        public IActionResult ContactList(long? team, string teamName, int pageNum = 0)
        {
            int bowlersPerPage = 5;

            return View(new ContactListViewModel
            {
                //grab and display data for the bowlers from the selected team
                Bowlers = (_context.Bowlers
                        .Where(b => b.TeamId == team || team == null)
                        .OrderBy(b => b.BowlerFirstName)
                        .Skip((pageNum - 1) * bowlersPerPage)
                        .Take(bowlersPerPage)
                        .ToList()),

                //assign page numbering info to the current selected page.
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumBowlersPerPage = bowlersPerPage,
                    CurrentPage = pageNum,
                    TotalBowlers = (team == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == team).Count())
                },

                //Create teamName to access in the default.cshtml and paginationTagHelper
                TeamName = teamName
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
