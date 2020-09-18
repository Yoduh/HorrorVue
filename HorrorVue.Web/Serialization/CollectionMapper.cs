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
	}
}
