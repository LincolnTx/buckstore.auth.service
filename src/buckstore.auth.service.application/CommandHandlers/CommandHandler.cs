using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using MediatR;

namespace buckstore.auth.service.application.CommandHandlers
{
	public abstract class CommandHandler
	{
		private readonly IUnitOfWork _uow;
		protected readonly IMediator _bus;
		private readonly ExceptionNotificationHandler _notifications;

		protected CommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications)
		{
			_uow = uow;
			_bus = bus;
			_notifications = (ExceptionNotificationHandler)notifications;
		}

		protected void NotifyValidationErrors(Command message)
		{
			foreach (var error in message.GetValidationResult().Errors)
			{
				_bus.Publish(new ExceptionNotification("001", error.ErrorMessage, error.PropertyName));
			}
		}

		public async Task<bool> Commit()
		{
			if (_notifications.HasNotifications()) return false;
			if (await _uow.Commit()) return true;

			await _bus.Publish(new ExceptionNotification("002", "We had a problem during saving your data."));

			return false;
		}
	}
}