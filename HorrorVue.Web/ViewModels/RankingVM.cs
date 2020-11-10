using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class RankingVM
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public int UserId { get; set; }
		public int CollectionId { get; set; }
		public List<int> Order { get; set; }
		public List<double> Ratings { get; set; }
	}
}
