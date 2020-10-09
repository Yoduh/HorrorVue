using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.InviteService
{
	public interface IInviteService
	{
		ServiceResponse<bool> CreateInvites(List<Data.Models.Invite> invites);
		List<Data.Models.Invite> GetInvitesForUserId(int id);
		ServiceResponse<bool> DeleteInviteById(int id);
	}
}
