using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

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
            return _context.Movies
                .Include(c=>c.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
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

        // POST /api/moveis
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
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _context.Movies.Single(c => c.Id == id);
            if (movieInDb==null)
            {
                return NotFound();

            }
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMoive(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
