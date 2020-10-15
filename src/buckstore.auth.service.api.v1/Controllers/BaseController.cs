using System.Collections.Generic;
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

		protected new IActionResult Response(int statusCode, object result = null)
		{
			if (IsValidOperation())
			{
				return StatusCode(statusCode, new
				{
					success = true,
					data = result
				});
			}

			return BadRequest(new
			{
				success = false,
				errors = _notifications.GetNotifications()
			});
		}
	}
}