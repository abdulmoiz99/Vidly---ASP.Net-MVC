using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private dbContext _context;

        public MoviesController()
        {
            _context = new dbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var moveies = _context.Movies.Include(c => c.Genre).ToList();
            return View(moveies);
        }
        public ActionResult Details(int Id)
        {
            var moveies = _context.Movies.SingleOrDefault(x => x.Id == Id);
            if (moveies == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(moveies);
            }
        }
        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}