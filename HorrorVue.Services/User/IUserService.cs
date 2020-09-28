using HorrorVue.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.User
{
	public interface IUserService
	{
		List<AppUser> GetAllUsers();
		AppUser GetUserById(string userId);
		AppUser GetUserByGoogleId(string userId);
		ServiceResponse<AppUser> CreateUser(AppUser user);
		ServiceResponse<bool> DeleteUser(string userId);
		//ServiceResponse<AppUser> AddCollectionForUserId(Data.Models.Collection collectionModel, string userId);
	}
}
