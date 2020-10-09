using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.Serialization
{
	public class InviteMapper
	{
		public static Invite SerializeInvite(InviteVM invite)
		{
			return new Invite
			{
				Id = invite.Id,
				CreatedOn = invite.CreatedOn,
				FromUserId = invite.FromUserId,
				FromUser = UserMapper.SerializeAppUser(invite.FromUser),
				ToUserId = invite.ToUserId,
				ToUser = UserMapper.SerializeAppUser(invite.ToUser),
				Collection = CollectionMapper.SerializeCollection(invite.Collection),
				CollectionId = invite.CollectionId
			};
		}
		public static InviteVM SerializeInvite(Invite invite)
		{
			return new InviteVM
			{
				Id = invite.Id,
				CreatedOn = invite.CreatedOn,
				FromUserId = invite.FromUserId,
				FromUser = UserMapper.SerializeAppUser(invite.FromUser),
				ToUserId = invite.ToUserId,
				ToUser = UserMapper.SerializeAppUser(invite.ToUser),
				Collection = CollectionMapper.SerializeCollection(invite.Collection),
				CollectionId = invite.CollectionId
			};
		}
		public static List<Invite> SerializeInvites(List<InviteVM> invites)
		{
			List<Invite> invs = new List<Invite>();
			foreach(InviteVM invite in invites)
			{
				invs.Add(SerializeInvite(invite));
			}
			return invs;
		}
		public static List<InviteVM> SerializeInvites(List<Invite> invites)
		{
			List<InviteVM> invs = new List<InviteVM>();
			foreach (Invite invite in invites)
			{
				invs.Add(SerializeInvite(invite));
			}
			return invs;
		}
	}
}
