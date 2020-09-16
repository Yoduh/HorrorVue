using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Movie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorrorVue.Web.Controllers
{
	[ApiController]
	public class MovieController : ControllerBase
	{
		private readonly ILogger<MovieController> _logger;
		private readonly IMovieService _movieService;

		public MovieController(ILogger<MovieController> logger, IMovieService movieService)
		{
			_logger = logger;
			_movieService = movieService;
		}

		[HttpGet("/api/movie")]
		public ActionResult GetMovies()
		{
			_logger.LogInformation("Getting all movies...");
			var movies = _movieService.GetAllMovies();
			return Ok(movies);
		}

		[HttpPost("/api/movie")]
		public ActionResult CreateMovie([FromBody] Movie movie)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_logger.LogInformation("Creating movie...");
			movie.CreatedOn = DateTime.UtcNow;
			movie.UpdatedOn = DateTime.UtcNow;
			ServiceResponse<Movie> createdMovie = _movieService.CreateMovie(movie);
			return Ok(createdMovie);
		}

		[HttpGet("/api/movie/{id}")]
		public ActionResult GetMovie(int id)
		{
			_logger.LogInformation($"Getting movie {id}");
			var response = _movieService.GetMovieById(id);
			return Ok(response);
		}

		[HttpDelete("/api/movie/{id}")]
		public ActionResult DeleteMovie(int id)
		{
			_logger.LogInformation($"Deleting movie {id}");
			var response = _movieService.DeleteMovie(id);
			return Ok(response);
		}
	}
}
