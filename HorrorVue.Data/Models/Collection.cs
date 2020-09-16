using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
	public class Collection
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public virtual List<Movie> Movies { get; set; } = new List<Movie>();
		public virtual List<Ranking> Rankings { get; set; } = new List<Ranking>();
	}
}
