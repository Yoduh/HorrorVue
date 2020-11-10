using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Ranking;
using HorrorVue.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorrorVue.Web.Controllers
{
	[ApiController]
	public class RankingController : ControllerBase
	{
		private readonly ILogger<RankingController> _logger;
		private readonly IRankingService _rankingService;

		public RankingController(ILogger<RankingController> logger, IRankingService rankingService)
		{
			_logger = logger;
			_rankingService = rankingService;
		}

		[HttpGet("/api/ranking")]
		public ActionResult GetRankings()
		{
			_logger.LogInformation("Getting all rankings...");
			var rankings = _rankingService.GetAllRankings();
			return Ok(rankings);
		}

		[HttpGet("/api/ranking/collections")]
		public ActionResult GetRankingsForCollections([FromQuery(Name = "collections[]")] List<int> collections)
		{
			_logger.LogInformation("Getting rankings for collections");
			List<Ranking> rankings = new List<Ranking>();
			foreach(int id in collections)
			{
				var ranking = _rankingService.GetRankingByCollectionId(id);
				if (ranking != null)
				{
					rankings.Add(ranking);
				}
			}
			return Ok(rankings);
		}

		[HttpPost("/api/ranking")]
		public ActionResult CreateRanking([FromBody] Ranking ranking)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_logger.LogInformation("Creating ranking...");
			ranking.CreatedOn = DateTime.UtcNow;
			ranking.UpdatedOn = DateTime.UtcNow;
			var createdRanking = _rankingService.CreateRanking(ranking);
			return Ok(createdRanking);
		}

		[HttpGet("/api/ranking/{id}")]
		public ActionResult GetRanking(int id)
		{
			_logger.LogInformation($"Getting ranking {id}");
			var response = _rankingService.GetRankingById(id);
			return Ok(response);
		}

		[HttpPatch("/api/ranking/{id}")]
		public ActionResult UpdateRanking([FromBody] Ranking ranking, int id)
		{
			_logger.LogInformation($"Getting ranking {id}");
			var dbRanking = _rankingService.GetRankingById(id);
			if (dbRanking == null)
				return BadRequest(new ServiceResponse<bool>
				{
					IsSuccess = false,
					Message = "Ranking ID not found",
					Time = DateTime.UtcNow,
					Data = false
				});
			dbRanking.Order = ranking.Order;
			dbRanking.Ratings = ranking.Ratings;
			dbRanking.UpdatedOn = DateTime.UtcNow;
			var response = _rankingService.UpdateRanking(dbRanking);
			return Ok(response);
		}

		[HttpDelete("/api/ranking/{id}")]
		public ActionResult DeleteRanking(int id)
		{
			_logger.LogInformation($"Deleting ranking {id}");
			var response = _rankingService.DeleteRanking(id);
			return Ok(response);
		}
	}
}
