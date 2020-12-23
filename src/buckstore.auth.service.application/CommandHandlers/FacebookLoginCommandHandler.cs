using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity;
using buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class FacebookLoginCommandHandler : CommandHandler, IRequestHandler<FacebookLoginCommand, LoginUserDto>
    {
        private readonly IFacebookIdendity _facebookIdentityService;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;

        public FacebookLoginCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications,
            IFacebookIdendity facebookIdendityService, IUserRepository userRepository, IIdentityService identityService) 
            : base(uow, bus, notifications)
        {
            _facebookIdentityService = facebookIdendityService;
            _userRepository = userRepository;
            _identityService = identityService;
        }

        public async Task<LoginUserDto> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return null;
            }

            var validateTokenResult = await _facebookIdentityService.ValidateAccessToken(request.AccessToken);
            if (! validateTokenResult.Data.IsValid)
            {
                await _bus.Publish(new ExceptionNotification("010", "Facebook access token inválido!"));
                return null;
            }

            var userInfo = await _facebookIdentityService.GetUserInfo(request.AccessToken);
            var checkUser = await _userRepository.FindUserByEmail(userInfo.Email);

            if (checkUser != null)
            {
                var authResult = _identityService.GenerateToken(checkUser);

                return new LoginUserDto(checkUser.Email, checkUser.Name, checkUser.Surname,
                    authResult.Token, authResult.RefreshToken);
            }

            var user = new User(userInfo.FirstName, userInfo.LastName, userInfo.Email, UserType.Customer.Id);

            _userRepository.Add(user);

            if (! await Commit())
            {
                await _bus.Publish(new ExceptionNotification("011", "Erro ao cadastrar este usuário!"));
                return null;
            }

            var authenticationResult = _identityService.GenerateToken(user);

            return new LoginUserDto(user.Email, user.Name, user.Surname,
                authenticationResult.Token, authenticationResult.RefreshToken);
        }
    }
}
