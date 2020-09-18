using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.Serialization
{
	public class MovieMapper
	{
		/// <summary>
		/// Serializes Movie VM => Movie Model
		/// </summary>
		/// <param name="movie"></param>
		/// <returns></returns>
		public static Movie SerializeMovie(MovieVM movie)
		{
			return new Movie
			{
				Id = movie.Id,
				CreatedOn = movie.CreatedOn,
				UpdatedOn = movie.UpdatedOn,
				TMDId = movie.tmdId,
				Title = movie.title,
				Description = movie.overview,
				ReleaseDate = movie.release_date,
				PosterImage = movie.poster_path
			};
		}

		public static List<Movie> SerializeMovies(IEnumerable<MovieVM> movieVMs)
		{
			List<Movie> movies = new List<Movie>();
			foreach (MovieVM movie in movieVMs)
			{
				movies.Add(SerializeMovie(movie));
			}
			return movies;
		}

		/// <summary>
		/// Serializes Movie Model => Movie VM
		/// </summary>
		/// <param name="movie"></param>
		/// <returns></returns>
		public static MovieVM SerializeMovie(Movie movie)
		{
			return new MovieVM
			{
				Id = movie.Id,
				CreatedOn = movie.CreatedOn,
				UpdatedOn = movie.UpdatedOn,
				tmdId = movie.TMDId,
				title = movie.Title,
				overview = movie.Description,
				release_date = movie.ReleaseDate,
				poster_path = movie.PosterImage
			};
		}

		public static List<MovieVM> SerializeMovies(IEnumerable<Movie> movieVMs)
		{
			List<MovieVM> movies = new List<MovieVM>();
			foreach (Movie movie in movieVMs)
			{
				movies.Add(SerializeMovie(movie));
			}
			return movies;
		}
	}
}
