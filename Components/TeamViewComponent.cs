using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext _context;
        public TeamViewComponent(BowlingLeagueContext con)
        {
            _context = con;
        }
        public IViewComponentResult Invoke()
        {
            //RouteData will grab the value teamName from the button clicked and store it to the SelectedTeam ViewBag
            //Nullify the ViewBag so it will load and display all bowlers when a specific team isn't specified
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            return View(_context.Teams
                        .Distinct()
                        .OrderBy(x => x));
        }
    }
}
