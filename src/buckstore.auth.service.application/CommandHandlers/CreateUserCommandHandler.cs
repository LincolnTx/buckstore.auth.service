using System;
using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using MediatR;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUnitOfWork uow, 
            IMediator bus, 
            INotificationHandler<ExceptionNotification> notifications,
            IUserRepository userRepository) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // validations
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return null;
            }
            // implementations and call repo
            
            // commit 
            if (await Commit())
            {
                //return user;
            }
            
            //return 
            return null;
        }
    }
}