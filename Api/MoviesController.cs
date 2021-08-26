using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Api
{
    public class MoviesController : ApiController
    {
        dbContext _context;
        public MoviesController()
        {
            _context = new dbContext();
        }
       
        //GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        // Get /api/movies/1
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        //[HttpPost]
        //public IHttpActionResult CreateMovie(MovieDto movieDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    var movie = Mapper.Map<MovieDto, Movie>(movieDto);
        //    _context.Movies.Add(movie);
        //    _context.SaveChanges();

        //    movieDto.Id = movie.Id;
        //    return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        //}


        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = Mapper.Map<MovieDto, Movie>(MovieDto);
            movie.GenreID = 2;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            MovieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), MovieDto);
        }
    }
}
