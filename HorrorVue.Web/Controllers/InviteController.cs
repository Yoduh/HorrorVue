using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorrorVue.Data.Models;
using HorrorVue.Services.InviteService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorrorVue.Web.Controllers
{
	[ApiController]
	public class InviteController : Controller
	{
		private readonly ILogger<InviteController> _logger;
		private readonly IInviteService _inviteService;

		public InviteController(ILogger<InviteController> logger, IInviteService inviteService)
		{
			_logger = logger;
			_inviteService = inviteService;
		}

		[HttpPost("/api/invite/")]
		public ActionResult CreateInvite([FromBody] List<Invite> invites)
		{
			_logger.LogInformation("Creating invite");
			var res = _inviteService.CreateInvites(invites);
			return Ok(res);
		}

		[HttpDelete("/api/invite/{id}")]
		public ActionResult RemoveInvite(int id)
		{
			_logger.LogInformation("Creating invite");
			var res = _inviteService.DeleteInviteById(id);
			return Ok(res);
		}
	}
}
