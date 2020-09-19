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
	}
}
