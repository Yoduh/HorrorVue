using HorrorVue.Data;
using HorrorVue.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorrorVue.Services.Collection
{
	public class CollectionService : ICollectionService
	{
		private HorrorDbContext _db;

		public CollectionService(HorrorDbContext dbContext)
		{
			_db = dbContext;
		}

		public ServiceResponse<Data.Models.Collection> CreateCollection(Data.Models.Collection collection)
		{
			try
			{
				_db.Collections.Add(collection);
				_db.SaveChanges();
				return new ServiceResponse<Data.Models.Collection>
				{
					IsSuccess = true,
					Message = "New collection added",
					Time = DateTime.UtcNow,
					Data = collection
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<Data.Models.Collection>
				{
					IsSuccess = false,
					Message = e.StackTrace,
					Time = DateTime.UtcNow,
					Data = collection
				};
			}
		}

		public ServiceResponse<bool> AddUserToCollection(int collectionId, string userId)
		{
			try
			{
				var dbColl = _db.Collections.First(i => i.Id == collectionId);
				var dbUser = _db.AppUsers.First(user => user.Id == int.Parse(userId));

				// you can either associate it using ids
				var appUserCollection = new AppUserCollection
				{
					CollectionId = dbColl.Id,
					AppUserId = dbUser.Id
				};

				_db.Add(appUserCollection);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					IsSuccess = true,
					Message = "Collection user updated",
					Time = DateTime.UtcNow,
					Data = true
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<bool>
				{
					IsSuccess = false,
					Message = e.StackTrace,
					Time = DateTime.UtcNow,
					Data = false
				};
			}
		}

		public ServiceResponse<bool> DeleteCollection(int collectionId)
		{
			var now = DateTime.UtcNow;
			try
			{
				var collection = _db.Collections.Find(collectionId);
				if (collection == null)
				{
					return new ServiceResponse<bool>
					{
						Time = DateTime.UtcNow,
						IsSuccess = false,
						Message = "Collection to delete not found!",
						Data = false
					};
				}
				_db.Remove(collection);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "collection deleted!",
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

		public ServiceResponse<Data.Models.Collection> UpdateCollection(List<Data.Models.Movie> movies, int id)
		{
			try
			{
				Data.Models.Collection collection = GetCollectionById(id);
				collection.UpdatedOn = DateTime.UtcNow;
				collection.Movies = movies;
				_db.Update(collection);
				_db.SaveChanges();
				return new ServiceResponse<Data.Models.Collection>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "Collection updated",
					Data = collection
				};
			} catch(Exception e)
			{
				return new ServiceResponse<Data.Models.Collection>
				{
					Time = DateTime.UtcNow,
					IsSuccess = false,
					Message = e.StackTrace,
					Data = null
				};
			}
		}

		public List<Data.Models.Collection> GetAllCollections()
		{
			return _db.Collections
				.Include(collection => collection.AppUsers)    // Join Relationship
				.ThenInclude(row => row.AppUser)
				.Include(collection => collection.Movies)
				.Include(collection => collection.Rankings)
				.OrderBy(collection => collection.Id)
				.ToList();
		}

		public Data.Models.Collection GetCollectionById(int collectionId)
		{
			return _db.Collections
				.Include(collection => collection.AppUsers)    // Join Relationship
				.ThenInclude(row => row.AppUser)
				.Include(collection => collection.Movies)
				.Include(collection => collection.Rankings)
				.First(collection => collection.Id == collectionId);
		}

		public List<Data.Models.Collection> GetCollectionsForUserId(string userId)
		{
			return new List<Data.Models.Collection>();
			//var collectionIds = _db.AppUserCollections
			//	.Where(auc => auc.AppUserId.Equals(userId))
			//	.Select(auc => auc.CollectionId)
			//	.ToList();

			//return _db.Collections
			//	.Include(collection => collection.AppUsers)    // Join Relationship
			//	.ThenInclude(row => row.AppUser)
			//	.Include(collection => collection.Movies)
			//	.Include(collection => collection.Rankings)
			//	.Where(collection => collectionIds.Contains(collection.Id))
			//	.ToList();
		}

		public List<Data.Models.Collection> GetCollectionsWithIds(List<int> collectionIds)
		{
			List<Data.Models.Collection> collections = new List<Data.Models.Collection>();
			foreach(int id in collectionIds)
			{
				collections.Add(GetCollectionById(id));
			}
			return collections;
		}
	}
}
