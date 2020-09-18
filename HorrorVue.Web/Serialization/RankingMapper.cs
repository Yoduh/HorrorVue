using HorrorVue.Data.Models;
using HorrorVue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorrorVue.Web.Serialization
{
	public class RankingMapper
	{
		/// <summary>
		/// Serializes Ranking VM => Ranking Model
		/// </summary>
		/// <param name="ranking"></param>
		/// <returns></returns>
		public static Ranking SerializeRanking(RankingVM ranking)
		{
			return new Ranking
			{
				Id = ranking.Id,
				CreatedOn = ranking.CreatedOn,
				UpdatedOn = ranking.UpdatedOn,
				UserId = ranking.UserId,
				CollectionId = ranking.CollectionId,
				Order = ranking.Order
			};
		}

		public static List<Ranking> SerializeRankings(IEnumerable<RankingVM> rankingVMs)
		{
			List<Ranking> rankings = new List<Ranking>();
			foreach (RankingVM ranking in rankingVMs)
			{
				rankings.Add(SerializeRanking(ranking));
			}
			return rankings;
		}

		/// <summary>
		/// Serializes Ranking Model => Ranking VM
		/// </summary>
		/// <param name="ranking"></param>
		/// <returns></returns>
		public static RankingVM SerializeRanking(Ranking ranking)
		{
			return new RankingVM
			{
				Id = ranking.Id,
				CreatedOn = ranking.CreatedOn,
				UpdatedOn = ranking.UpdatedOn,
				UserId = ranking.UserId,
				CollectionId = ranking.CollectionId,
				Order = ranking.Order
			};
		}
		public static List<RankingVM> SerializeRankings(IEnumerable<Ranking> rankings)
		{
			List<RankingVM> rankingVMs = new List<RankingVM>();
			foreach (Ranking ranking in rankings)
			{
				rankingVMs.Add(SerializeRanking(ranking));
			}
			return rankingVMs;
		}
	}
}
