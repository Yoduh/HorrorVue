using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Collection;
using HorrorVue.Services.User;
using HorrorVue.Web.Serialization;
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
		private readonly IUserService _userService;

		public CollectionController(ILogger<CollectionController> logger, ICollectionService collectionService,
			IUserService userService)
		{
			_logger = logger;
			_collectionService = collectionService;
			_userService = userService;
		}

		[HttpGet("/api/collection")]
		public ActionResult GetCollections()
		{
			_logger.LogInformation("Getting all collections...");
			var collections = _collectionService.GetAllCollections();
			return Ok(collections);
		}

		[HttpPost("/api/collection")]
		public ActionResult CreateCollection([FromBody] CollectionVM collection)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_logger.LogInformation("Creating collection...");
			collection.CreatedOn = DateTime.UtcNow;
			collection.UpdatedOn = DateTime.UtcNow;
			var collectionModel = CollectionMapper.SerializeCollection(collection);
			var user = _userService.GetUserByGoogleId(collection.UserId);
			var collectionToAdd = new AppUserCollection { AppUserId = user.Id, CollectionId = collectionModel.Id };
			collectionModel.AppUsers.Add(collectionToAdd);
			ServiceResponse<Collection> createdCollection = _collectionService.CreateCollection(collectionModel);
			//var result = _userService.AddCollectionForUserId(collectionModel, collection.UserId);
			return Ok();// (createdCollection);
		}

		[HttpPatch("/api/collection/{collectionId}/user/{userId}")]
		public ActionResult AddUserToCollection(int collectionId, int userId)
		{
			_logger.LogInformation("Updating collection with new user");
			var result = _collectionService.AddUserToCollection(collectionId, userId.ToString());
			if (result.Data)
				return Ok();
			else
				return NotFound(); // find something better to return
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
