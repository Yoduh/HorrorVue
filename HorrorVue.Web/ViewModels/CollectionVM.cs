using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class CollectionVM
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string Name { get; set; }
		public int CreatedBy { get; set; }
		public virtual List<MovieVM> Movies { get; set; } = new List<MovieVM>();
		public virtual List<RankingVM> Rankings { get; set; } = new List<RankingVM>();
		public virtual IEnumerable<AppUserVM> AppUsers { get; set; }
	}
}
