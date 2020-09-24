using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Collections.Generic;
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
		public static AppUser SerializeAppUser(AppUserVM user)
		{
			return new AppUser
			{
				Id = user.Id,
				CreatedOn = user.CreatedOn,
				UpdatedOn = user.UpdatedOn,
				FirstName = user.FirstName,
				LastName = user.LastName,
				GoogleId = user.GoogleId,
				//Collections = CollectionMapper.SerializeCollections(user.Collections),
			};
		}

		internal static IEnumerable<AppUser> SerializeAppUsers(IEnumerable<AppUserVM> appUsers)
		{
			List<AppUser> users = new List<AppUser>();
			foreach(AppUserVM user in appUsers)
			{
				users.Add(SerializeAppUser(user));
			}
			return users;
		}

		/// <summary>
		/// Serializes AppUser Model => AppUser VM
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static AppUserVM SerializeAppUser(AppUser user)
		{
			return new AppUserVM
			{
				Id = user.Id,
				CreatedOn = user.CreatedOn,
				UpdatedOn = user.UpdatedOn,
				FirstName = user.FirstName,
				LastName = user.LastName,
				GoogleId = user.GoogleId,
				Collections = new List<CollectionVM>()
			};
		}

		internal static IEnumerable<AppUserVM> SerializeAppUsers(IEnumerable<AppUser> appUsers)
		{
			List<AppUserVM> users = new List<AppUserVM>();
			foreach (AppUser user in appUsers)
			{
				users.Add(SerializeAppUser(user));
			}
			return users;
		}

		/// <summary>
		/// Serializes AppUserCollection Model => AppUser VM
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static AppUserVM SerializeAppUserCollection(AppUserCollection user)
		{
			return new AppUserVM
			{
				Id = user.AppUser.Id,
				CreatedOn = user.AppUser.CreatedOn,
				UpdatedOn = user.AppUser.UpdatedOn,
				FirstName = user.AppUser.FirstName,
				LastName = user.AppUser.LastName,
				GoogleId = user.AppUser.GoogleId,
				Collections = new List<CollectionVM>()
			};
		}

		internal static IEnumerable<AppUserVM> SerializeAppUserCollections(IEnumerable<AppUserCollection> appUsers)
		{
			List<AppUserVM> users = new List<AppUserVM>();
			foreach (AppUserCollection user in appUsers)
			{
				users.Add(SerializeAppUserCollection(user));
			}
			return users;
		}
	}
}
