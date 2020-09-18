using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.Serialization
{
	public class UserMapper
	{
		/// <summary>
		/// Serializes AppUser VM => AppUser Model
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static AppUser SerialiazeAppUser(AppUserVM user)
		{
			return new AppUser
			{
				Id = user.Id,
				CreatedOn = user.CreatedOn,
				UpdatedOn = user.UpdatedOn,
				FirstName = user.FirstName,
				LastName = user.LastName,
				GoogleId = user.GoogleId,
				Collections = CollectionMapper.SerializeCollections(user.Collections),
			};
		}

		/// <summary>
		/// Serializes AppUser Model => AppUser VM
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static AppUserVM SerialiazeAppUser(AppUser user)
		{
			return new AppUserVM
			{
				Id = user.Id,
				CreatedOn = user.CreatedOn,
				UpdatedOn = user.UpdatedOn,
				FirstName = user.FirstName,
				LastName = user.LastName,
				GoogleId = user.GoogleId,
				Collections = CollectionMapper.SerializeCollections(user.Collections),
			};
		}
	}
}
