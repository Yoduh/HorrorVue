using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.Collection
{
	public interface ICollectionService
	{
		List<Data.Models.Collection> GetAllCollections();
		Data.Models.Collection GetCollectionById(int collectionId);
		ServiceResponse<Data.Models.Collection> CreateCollection(Data.Models.Collection collection);
		ServiceResponse<bool> AddUserToCollection(int collectionId, string userId);
		ServiceResponse<bool> RemoveUserFromCollection(int collectionId, int userId);
		ServiceResponse<bool> DeleteCollection(int collectionId);
		ServiceResponse<Data.Models.Collection> UpdateCollection(List<Data.Models.Movie> movies, int id, string name);
		List<Data.Models.Collection> GetCollectionsForUserId(string userId);
		List<Data.Models.Collection> GetCollectionsWithIds(List<int> collections);
	}
}
