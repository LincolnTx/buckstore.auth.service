using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.application.IntegrationEvents.Events;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using MediatR;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUnitOfWork uow, 
            IMediator bus, 
            INotificationHandler<ExceptionNotification> notifications,
            IUserRepository userRepository, IMapper mapper) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return null;
            }
            
            var user = new User(request.Name, request.Surname, request.Email, request.Password, request.Cpf);
            var userDto = _mapper.Map<CreateUserDto>(user);
            
            _userRepository.Add(user);

            if (await Commit())
            {
                var userIntegrationEvent =new UserCreatedIntegrationEvent(userDto);
                await _bus.Publish(userIntegrationEvent, cancellationToken);
                
                return userDto;
            }
            
            return null;
        }
    }
}