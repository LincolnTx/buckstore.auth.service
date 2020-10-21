using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity;
using MediatR;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class LoginUserCommandHandler : CommandHandler, IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        
        public LoginUserCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications,
            IUserRepository userRepository, IIdentityService identityService) 
            : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _identityService = identityService;
        }

        public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return null;
            }

            var user = await _userRepository.FindUserByEmail(request.Email);
            var isPasswordValid = user.VerifyUserPassword(request.Email, request.Password);

            if (!isPasswordValid) return null;

            var authenticationResult = _identityService.GenerateToken(user.Id.ToString(), user._email);
            
            
            return new LoginUserDto(user.GetEmail, user.GetName, 
                authenticationResult.Token, authenticationResult.RefreshToken);
        }
    }
}