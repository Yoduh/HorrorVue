using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.ViewModels
{
	public class InviteVM
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public int FromUserId { get; set; }
		public AppUserVM FromUser { get; set; }
		public int ToUserId { get; set; }
		public AppUserVM ToUser { get; set; }
		public CollectionVM Collection { get; set; }
		public int CollectionId { get; set; }
	}
}
