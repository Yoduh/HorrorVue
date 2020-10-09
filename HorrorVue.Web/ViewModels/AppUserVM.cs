using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class AppUserVM
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string GoogleId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public virtual IEnumerable<CollectionVM> Collections { get; set; }
		public virtual IEnumerable<InviteVM> Invites { get; set; }
	}
}
