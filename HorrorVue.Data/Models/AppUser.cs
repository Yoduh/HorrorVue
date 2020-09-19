using System;
using System.Collections.Generic;

namespace HorrorVue.Data.Models
{
	public class AppUser
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string GoogleId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<AppUserCollection> Collections { get; set; }
	}
}
