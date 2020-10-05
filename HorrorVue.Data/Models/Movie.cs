using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public int TMDId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ReleaseDate { get; set; }
		public string PosterImage { get; set; }
		public Collection Collection { get; set; }
	}
}
