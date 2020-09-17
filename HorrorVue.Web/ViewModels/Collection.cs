using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class Collection
	{
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public virtual List<Movie> Movies { get; set; } = new List<Movie>();
		public virtual List<Ranking> Rankings { get; set; } = new List<Ranking>();
	}
}
