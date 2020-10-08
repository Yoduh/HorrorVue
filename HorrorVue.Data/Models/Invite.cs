using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
	public class Invite
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public int FromUserId { get; set; }
		public AppUser FromUser { get; set; }
		public int ToUserId { get; set; }
		public AppUser ToUser { get; set; }
		public Collection Collection { get; set; }
		public int CollectionId { get; set; }
	}
}
