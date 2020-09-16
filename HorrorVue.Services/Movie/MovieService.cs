using HorrorVue.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorrorVue.Services.Movie
{
	public class MovieService : IMovieService
	{
		private HorrorDbContext _db;

		public MovieService(HorrorDbContext dbContext)
		{
			_db = dbContext;
		}

		public ServiceResponse<Data.Models.Movie> CreateMovie(Data.Models.Movie movie)
		{
			try
			{
				_db.Movies.Add(movie);
				_db.SaveChanges();
				return new ServiceResponse<Data.Models.Movie>
				{
					IsSuccess = true,
					Message = "New movie added",
					Time = DateTime.UtcNow,
					Data = movie
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<Data.Models.Movie>
				{
					IsSuccess = false,
					Message = e.StackTrace,
					Time = DateTime.UtcNow,
					Data = movie
				};
			}
		}

		public ServiceResponse<bool> DeleteMovie(int movieId)
		{
			var now = DateTime.UtcNow;
			try
			{
				var movie = _db.Movies.Find(movieId);
				if (movie == null)
				{
					return new ServiceResponse<bool>
					{
						Time = DateTime.UtcNow,
						IsSuccess = false,
						Message = "Movie to delete not found!",
						Data = false
					};
				}
				_db.Remove(movieId);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "movie deleted!",
					Data = true
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = false,
					Message = e.StackTrace,
					Data = false
				};
			}
		}

		public List<Data.Models.Movie> GetAllMovies()
		{
			return _db.Movies
				.OrderBy(movie => movie.Title)
				.ToList();
		}

		public Data.Models.Movie GetMovieById(int movieId)
		{
			return _db.Movies
				.First(movie => movie.Id == movieId);
		}
	}
}
