using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Services;
using HorrorVue.Services.Collection;
using HorrorVue.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorrorVue.Web.Controllers
{
	[ApiController]
	public class CollectionController : ControllerBase
	{
		private readonly ILogger<CollectionController> _logger;
		private readonly ICollectionService _collectionService;

		public CollectionController(ILogger<CollectionController> logger, ICollectionService collectionService)
		{
			_logger = logger;
			_collectionService = collectionService;
		}

		[HttpGet("/api/collection")]
		public ActionResult GetCollections()
		{
			_logger.LogInformation("Getting all collections...");
			var collections = _collectionService.GetAllCollections();
			return Ok(collections);
		}

		[HttpPost("/api/collection")]
		public ActionResult CreateCollection([FromBody] Collection collection)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_logger.LogInformation("Creating collection...");
			collection.CreatedOn = DateTime.UtcNow;
			collection.UpdatedOn = DateTime.UtcNow;
			//ServiceResponse<Collection> createdCollection = _collectionService.CreateCollection(collection);
			return Ok();// createdCollection);
		}

		[HttpGet("/api/collection/{id}")]
		public ActionResult GetCollection(int id)
		{
			_logger.LogInformation($"Getting collection {id}");
			var response = _collectionService.GetCollectionById(id);
			return Ok(response);
		}

		[HttpDelete("/api/collection/{id}")]
		public ActionResult DeleteCollection(int id)
		{
			_logger.LogInformation($"Deleting collection {id}");
			var response = _collectionService.DeleteCollection(id);
			return Ok(response);
		}
	}
}
