﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string Email { get; set; }
		public ICollection<Invite> SentInvites { get; set; }
		public ICollection<Invite> ReceivedInvites { get; set; }
		public ICollection<AppUserCollection> Collections { get; set; }
	}
}
