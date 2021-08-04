using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.application.IntegrationEvents.Events;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class UserAdditionalInfoCommandHandler : CommandHandler, IRequestHandler<UserAdditionalInfoCommand>
    {
        private readonly IUserRepository _userRepository;
        public UserAdditionalInfoCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications, IUserRepository userRepository) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UserAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userRepository.FindUserById(request.UserId);

            if (user.UserInformationSet())
                return Unit.Value;

            user.AddUserCpf(request.Cpf);
            //user.AddCredCardForUser(request.CredCard);

            if (!await Commit())
                await _bus.Publish(new ExceptionNotification("005",
                    "Erro ao adicionar informações do usuário, tente novamente mais tarde!"),
                    cancellationToken);

            await _bus.Publish(new BuyerCreatedIntegrationEvent(user.Name, user.Cpf, user.Id), cancellationToken);

            return Unit.Value;
        }
    }
}
