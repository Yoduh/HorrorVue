using HorrorVue.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorrorVue.Services.Ranking
{
	public class RankingService : IRankingService
	{
		private HorrorDbContext _db;

		public RankingService(HorrorDbContext dbContext)
		{
			_db = dbContext;
		}
		public ServiceResponse<Data.Models.Ranking> CreateRanking(Data.Models.Ranking ranking)
		{
			try
			{
				_db.Rankings.Add(ranking);
				_db.SaveChanges();
				return new ServiceResponse<Data.Models.Ranking>
				{
					IsSuccess = true,
					Message = "New ranking added",
					Time = DateTime.UtcNow,
					Data = ranking
				};
			}
			catch (Exception e)
			{
				return new ServiceResponse<Data.Models.Ranking>
				{
					IsSuccess = false,
					Message = e.StackTrace,
					Time = DateTime.UtcNow,
					Data = ranking
				};
			}
		}

		public ServiceResponse<bool> DeleteRanking(int rankingId)
		{
			var now = DateTime.UtcNow;
			try
			{
				var ranking = _db.Rankings.Find(rankingId);
				if (ranking == null)
				{
					return new ServiceResponse<bool>
					{
						Time = DateTime.UtcNow,
						IsSuccess = false,
						Message = "Ranking to delete not found!",
						Data = false
					};
				}
				_db.Remove(rankingId);
				_db.SaveChanges();
				return new ServiceResponse<bool>
				{
					Time = DateTime.UtcNow,
					IsSuccess = true,
					Message = "ranking deleted!",
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

		public List<Data.Models.Ranking> GetAllRankings()
		{
			return _db.Rankings
				.OrderBy(ranking => ranking.UserId)
				.ToList();
		}

		public Data.Models.Ranking GetRankingByCollectionId(int collectionId)
		{
			try
			{
				return _db.Rankings
					.First(ranking => ranking.CollectionId == collectionId);
			} catch (Exception e)
			{
				return null;
			}
		}

		public Data.Models.Ranking GetRankingById(int rankingId)
		{
			return _db.Rankings
				.First(ranking => ranking.Id == rankingId);
		}
	}
}
