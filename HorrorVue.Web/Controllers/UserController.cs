using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services;
using HorrorVue.Services.Collection;
using HorrorVue.Services.User;
using HorrorVue.Web.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorrorVue.Web.Controllers
{
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly IUserService _userService;
		private readonly ICollectionService _collectionService;

		public UserController(ILogger<UserController> logger, IUserService userService, ICollectionService collectionService)
		{
			_logger = logger;
			_userService = userService;
			_collectionService = collectionService;
		}

		[HttpGet("/api/user")]
		public ActionResult GetUsers()
		{
			_logger.LogInformation("Getting all users...");
			var users = _userService.GetAllUsers();
			return Ok(users);
		}

		[HttpPost("/api/user")]
		public ActionResult CreateUser([FromBody] AppUser user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_logger.LogInformation("Creating user...");
			user.CreatedOn = DateTime.UtcNow;
			user.UpdatedOn = DateTime.UtcNow;
			ServiceResponse<AppUser> createdUser = _userService.CreateUser(user);
			return Ok(createdUser);
		}

		[HttpGet("/api/user/{id}")]
		public ActionResult GetUser(string id)
		{
			_logger.LogInformation($"Getting user {id}");
			var user = _userService.GetUserByGoogleId(id);
			var ids = user.Collections.Select(row => row.CollectionId).ToList();
			var collections = _collectionService.GetCollectionsWithIds(ids);
			var response = UserMapper.SerializeAppUser(user);
			response.Collections = CollectionMapper.SerializeCollections(collections);
			return Ok(response);
		}

		[HttpDelete("/api/user/{id}")]
		public ActionResult DeleteUser(string id)
		{
			_logger.LogInformation($"Deleting user {id}");
			var response = _userService.DeleteUser(id);
			return Ok(response);
		}
	}
}
