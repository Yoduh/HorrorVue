using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.Movie
{
	public interface IMovieService
	{
		List<Data.Models.Movie> GetAllMovies();
		Data.Models.Movie GetMovieById(int movieId);
		ServiceResponse<Data.Models.Movie> CreateMovie(Data.Models.Movie movie);
		ServiceResponse<bool> DeleteMovie(int movieId);
	}
}
