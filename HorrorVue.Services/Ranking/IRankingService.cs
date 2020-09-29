using System;
using System.Collections.Generic;
using System.Text;

namespace HorrorVue.Services.Ranking
{
	public interface IRankingService
	{
		List<Data.Models.Ranking> GetAllRankings();
		Data.Models.Ranking GetRankingById(int rankingId);
		Data.Models.Ranking GetRankingByCollectionId(int collectionId);
		ServiceResponse<Data.Models.Ranking> CreateRanking(Data.Models.Ranking ranking);
		ServiceResponse<bool> DeleteRanking(int rankingId);
		Data.Models.Ranking UpdateRanking(Data.Models.Ranking id);
	}
}
