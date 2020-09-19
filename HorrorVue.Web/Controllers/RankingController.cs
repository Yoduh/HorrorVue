﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Ranking;
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
			ServiceResponse<Ranking> createdRanking = _rankingService.CreateRanking(ranking);
			return Ok(createdRanking);
		}

		[HttpGet("/api/ranking/{id}")]
		public ActionResult GetRanking(int id)
		{
			_logger.LogInformation($"Getting ranking {id}");
			var response = _rankingService.GetRankingById(id);
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