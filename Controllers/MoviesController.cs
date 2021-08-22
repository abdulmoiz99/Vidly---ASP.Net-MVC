using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var ViewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", ViewModel);
        }
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.Single(c => c.Id == Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
        public ActionResult Save(Movie movie)
        {
            var genre = _context.Genres.SingleOrDefault(c => c.Id == movie.Genre.Id);
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
               
                movie.Genre = genre;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }
    }
}