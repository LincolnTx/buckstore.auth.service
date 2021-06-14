using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class ChangeUserPasswordCommandHandler : CommandHandler, IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRespository;
        public ChangeUserPasswordCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications,
            IUserRepository userRepository) 
            : base(uow, bus, notifications)
        {
            _userRespository = userRepository;
        }
        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (! request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }
            
            var user = await _userRespository.FindUserByEmail(request.Email);

            if (! user.VerifyUserPassword(request.CurrentPassword))
            {
                await _bus.Publish(new ExceptionNotification("003", "Você não está autorizado a trocar a senha deste usuário"));

                return false;
            }

            user.ChangePassword(request.NewPassword);

            if (! await Commit())
            {
                await _bus.Publish(new ExceptionNotification("004", 
                    "Erro ao trocar sua senha, tente novamente mais tarde!"), 
                    cancellationToken);
                return false;
            }

            return true;
        }
    }
}
