using HorrorVue.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.User
{
	public interface IUserService
	{
		List<AppUser> GetAllUsers();
		AppUser GetUserById(int userId);
		ServiceResponse<AppUser> CreateUser(AppUser user);
		ServiceResponse<bool> DeleteUser(int userId);
	}
}
