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

		public ServiceResponse<bool> DeleteUser(int id)
		{
			var now = DateTime.UtcNow;
			try
			{
				var customer = _db.AppUsers.Find(id);
				if (customer == null)
				{
					return new ServiceResponse<bool>
					{
						Time = DateTime.UtcNow,
						IsSuccess = false,
						Message = "User to delete not found!",
						Data = false
					};
				}
				_db.Remove(customer);
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
				.Include(user => user.Collections)
				.OrderBy(user => user.LastName)
				.ToList();
		}

		public AppUser GetUserById(int userId)
		{
			return _db.AppUsers
				.Include(user => user.Collections)
				.First(user => user.Id == userId);
		}
	}
}
