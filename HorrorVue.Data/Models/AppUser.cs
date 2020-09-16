using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
	public class AppUser
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual List<Collection> Collections { get; set; } = new List<Collection>();
	}
}
