using HorrorVue.Data;
using HorrorVue.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorrorVue.Services.User
{
	public class UserService : IUserService
	{
		private HorrorDbContext _db;

		public UserService(HorrorDbContext dbContext)
		{
			_db = dbContext;
		}
		public ServiceResponse<AppUser> CreateUser(AppUser user)
		{
			try
			{
				_db.AppUsers.Add(user);
				_db.SaveChanges();
				return new ServiceResponse<AppUser>
				{
					IsSuccess = true,
					Message = "New user added",
					Time = DateTime.UtcNow,
					Data = user
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<Data.Models.AppUser>
				{
					IsSuccess = false,
					Message = e.StackTrace,
					Time = DateTime.UtcNow,
					Data = user
				};
			}
		}

		public ServiceResponse<bool> DeleteUser(string id)
		{
			var now = DateTime.UtcNow;
			try
			{
				var user = _db.AppUsers.Find(id);
				if (user == null)
				{
					return new ServiceResponse<bool>
					{
						Time = DateTime.UtcNow,
						IsSuccess = false,
						Message = "User to delete not found!",
						Data = false
					};
				}
				_db.Remove(user);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "user deleted!",
					Data = true
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = false,
					Message = e.StackTrace,
					Data = false
				};
			}
		}

		public List<AppUser> GetAllUsers()
		{
			return _db.AppUsers
				.Include(user => user.Collections)    // Join Relationship
				.ThenInclude(row => row.Collection)
				.OrderBy(user => user.LastName)
				.ToList();
		}

		public AppUser GetUserById(string userId)
		{
			try
			{
				return _db.AppUsers
					.Include(user => user.Collections)    // Join Relationship
					.ThenInclude(row => row.Collection)
					.First(user => user.Id.Equals(userId));
			}
			catch (InvalidOperationException e)
			{
				return null;
			}
		}

		public AppUser GetUserByGoogleId(string userId)
		{
			try
			{
				return _db.AppUsers
					.Include(user => user.Collections)    // Join Relationship
					.ThenInclude(row => row.Collection)
					.First(user => user.GoogleId.Equals(userId));
			} catch(InvalidOperationException e)
			{
				return null;
			}
		}

		//public ServiceResponse<AppUser> AddCollectionForUserId(Data.Models.Collection collection, string userId)
		//{
		//	try
		//	{
		//		AppUser user = GetUserByGoogleId(userId);
		//		if (user == null)
		//		{
		//			return new ServiceResponse<AppUser>
		//			{
		//				Time = DateTime.UtcNow,
		//				IsSuccess = false,
		//				Message = "User to delete not found!",
		//				Data = null
		//			};
		//		}

		//		user.AppUserCollections.Add(new AppUserCollection {
		//			AppUser = user,
		//			Collection = collection
		//		});
		//		_db.AppUsers.Update(user);
		//		_db.SaveChanges();
		//		return new ServiceResponse<AppUser>
		//		{
		//			Time = DateTime.UtcNow,
		//			IsSuccess = true,
		//			Message = "user deleted!",
		//			Data = user
		//		};
		//	}
		//	catch (Exception e)
		//	{
		//		return new ServiceResponse<AppUser>
		//		{
		//			Time = DateTime.UtcNow,
		//			IsSuccess = false,
		//			Message = e.StackTrace,
		//			Data = null
		//		};
		//	}
		//}
	}
}
