﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using buckstore.auth.service.api.v1.Filters;
 using buckstore.auth.service.domain.Exceptions;
 using MediatR;
 using Microsoft.AspNetCore.Mvc;

namespace buckstore.auth.service.api.v1.Controllers
{
	[Route("auth/[controller]")]
	[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
	public class BaseController : Controller
	{
		private readonly ExceptionNotificationHandler _notifications;

		protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

		protected BaseController(INotificationHandler<ExceptionNotification> notifications)
		{
			_notifications = (ExceptionNotificationHandler) notifications;
		}

		protected bool IsValidOperation()
		{
			return (!_notifications.HasNotifications());
		}

        protected new IActionResult Response(IActionResult action)
		{
            if (IsValidOperation())
            {
                return action;
            }

            return BadRequest
            (
                new
                {
                    success = false,
                    errors = _notifications.GetNotifications()
                }
            );
        }

		protected string GetTokenClaim(string claim)
		{
			var header = Request.Headers["Authorization"].ToString();
			var token = header.Replace("Bearer ", string.Empty);

			var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

			return readToken.Payload[claim].ToString() ?? string.Empty;
		}
	}
}
