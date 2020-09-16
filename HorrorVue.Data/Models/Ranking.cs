using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
	public class Ranking
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public AppUser User { get; set; }
		public Collection Collection { get; set; }
		public List<int> Order { get; set; }
	}
}
