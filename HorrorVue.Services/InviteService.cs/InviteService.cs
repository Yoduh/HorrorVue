using HorrorVue.Data;
using HorrorVue.Data.Models;
using HorrorVue.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorrorVue.Services.InviteService
{
	public class InviteService : IInviteService
	{
		private HorrorDbContext _db;

		public InviteService(HorrorDbContext dbContext)
		{
			_db = dbContext;
		}

		public ServiceResponse<bool> CreateInvites(List<Invite> invites)
		{
			try
			{
				var fromUser = _db.AppUsers.First(user => user.Id == invites.First().FromUserId);
				foreach (Invite invite in invites)
				{
					invite.FromUserId = fromUser.Id;
					var toUser = _db.AppUsers
						.Include(u => u.Collections)
						.First(user => user.Email.ToLower().Equals(invite.ToUserEmail.ToLower()));
					// skip if user already has collection
					if (toUser.Collections.FirstOrDefault(au => au.CollectionId == invite.CollectionId) != null)
					{
						continue;
					}
					// update invite if already exists
					var existingInvite = _db.Invite.FirstOrDefault(i => i.ToUserId == toUser.Id && i.CollectionId == invite.CollectionId);
					if (existingInvite != null)
					{
						existingInvite.CreatedOn = DateTime.UtcNow;
						_db.Invite.Update(existingInvite);
						continue;
					}

					invite.ToUserId = toUser.Id;
					invite.CreatedOn = DateTime.UtcNow;

					_db.Invite.Add(invite);
				}
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "Invites saved",
					Data = true
				};
			} catch (Exception e)
			{
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = false,
					Message = e.Message,
					Data = false
				};
			}
		}

		public ServiceResponse<bool> DeleteInviteById(int id)
		{
			try
			{
				var invite = _db.Invite.First(invite => invite.Id == id);
				_db.Invite.Remove(invite);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "Invite deleted",
					Data = true
				};
			} catch(Exception e)
			{
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = false,
					Message = e.Message,
					Data = false
				};
			}
		}

		public List<Invite> GetInvitesForUserId(int id)
		{
			return _db.Invite
				.Include(invite => invite.FromUser)
				.Include(invite => invite.Collection)
				.ThenInclude(c => c.AppUsers)
				.ThenInclude(row => row.AppUser)
				.Where(invite => invite.ToUserId == id)
				.ToList();
		}
	}
}
