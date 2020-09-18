using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class MovieVM
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string poster_path { get; set; }
		public bool adult { get; set; }
		public string overview { get; set; }
		public string release_date { get; set; }
		public int[] genre_ids { get; set; }
		public int tmdId { get; set; }
		public string original_title { get; set; }
		public string original_language { get; set; }
		public string title { get; set; }
		public string backdrop_path { get; set; }
		public double popularity { get; set; }
		public int vote_count { get; set; }
		public bool video { get; set; }
		public double vote_average { get; set; }
	}
}
