using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Collection;
using HorrorVue.Services.InviteService;
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
		private readonly IInviteService _inviteService;

		public CollectionController(ILogger<CollectionController> logger, ICollectionService collectionService,
			IUserService userService, IInviteService inviteService)
		{
			_logger = logger;
			_collectionService = collectionService;
			_userService = userService;
			_inviteService = inviteService;
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
			var user = _userService.GetUserById(collection.CreatedBy.ToString());
			var collectionToAdd = new AppUserCollection { AppUserId = user.Id, CollectionId = collectionModel.Id };
			collectionModel.AppUsers.Add(collectionToAdd);
			ServiceResponse<Collection> createdCollection = _collectionService.CreateCollection(collectionModel);
			var response = new ServiceResponse<CollectionVM>
			{
				IsSuccess = createdCollection.IsSuccess,
				Message = createdCollection.Message,
				Time = createdCollection.Time,
				Data = CollectionMapper.SerializeCollection(createdCollection.Data)
			};
			return Ok(response);
		}

		[HttpPatch("/api/collection/{collectionId}/user/{userId}")]
		public ActionResult AddUserToCollection([FromBody] Invite invite, int collectionId, int userId)
		{
			_logger.LogInformation("Updating collection with new user");
			var result = _collectionService.AddUserToCollection(collectionId, userId.ToString());
			if (result.IsSuccess)
			{
				_inviteService.DeleteInviteById(invite.Id);
				var collection = _collectionService.GetCollectionById(collectionId);
				var res = new ServiceResponse<CollectionVM>
				{
					IsSuccess = result.IsSuccess,
					Message = result.Message,
					Time = result.Time,
					Data = CollectionMapper.SerializeCollection(collection)
				};
				return Ok(res);
			}
			else
				return Ok(result);
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

		[HttpPatch("/api/collection/{id}")]
		public ActionResult UpdateCollection([FromBody] List<MovieVM> movies, int id)
		{
			_logger.LogInformation($"Updating collection {id}");
			var movieModels = MovieMapper.SerializeMovies(movies);
			var updatedCollection = _collectionService.UpdateCollection(movieModels, id);
			var response = new ServiceResponse<CollectionVM>
			{
				IsSuccess = updatedCollection.IsSuccess,
				Message = updatedCollection.Message,
				Time = updatedCollection.Time,
				Data = CollectionMapper.SerializeCollection(updatedCollection.Data)
			};
			return Ok(response);
		}
	}
}
