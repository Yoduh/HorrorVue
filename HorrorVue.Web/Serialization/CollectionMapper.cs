using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.Serialization
{
	public class CollectionMapper
	{
		/// <summary>
		/// Serializes Collection VM => Collection Model
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static Collection SerializeCollection(CollectionVM collection)
		{
			return new Collection
			{
				Id = collection.Id,
				CreatedOn = collection.CreatedOn,
				UpdatedOn = collection.UpdatedOn,
				Name = collection.Name,
				Movies = MovieMapper.SerializeMovies(collection.Movies),
				Rankings = RankingMapper.SerializeRankings(collection.Rankings)
			};
		}

		internal static List<Collection> SerializeCollections(List<CollectionVM> collectionVMs)
		{
			List<Collection> collections = new List<Collection>();
			foreach (CollectionVM collection in collectionVMs)
			{
				collections.Add(SerializeCollection(collection));
			}
			return collections;
		}

		/// <summary>
		/// Serializes Collection => Collection VM
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static CollectionVM SerializeCollection(Collection collection)
		{
			return new CollectionVM
			{
				Id = collection.Id,
				CreatedOn = collection.CreatedOn,
				UpdatedOn = collection.UpdatedOn,
				Name = collection.Name,
				Movies = MovieMapper.SerializeMovies(collection.Movies),
				Rankings = RankingMapper.SerializeRankings(collection.Rankings)
			};
		}

		internal static List<CollectionVM> SerializeCollections(List<Collection> collections)
		{
			List<CollectionVM> collectionVMs = new List<CollectionVM>();
			foreach (Collection collection in collections)
			{
				collectionVMs.Add(SerializeCollection(collection));
			}
			return collectionVMs;
		}
	}
}
