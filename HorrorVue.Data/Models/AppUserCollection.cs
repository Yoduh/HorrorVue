using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Data.Models
{
/// <summary>
/// This class serves as the join table between AppUser and Collection necessary for forming the many-to-many relationship
/// </summary>
	public class AppUserCollection
	{
		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int CollectionId { get; set; }
		public Collection Collection { get; set; }
	}
}
