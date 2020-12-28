using AutoMapper;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace buckstore.auth.service.application.CommandHandlers
{
    public class CreateEmployeeCommandHandler : CommandHandler, IRequestHandler<CreateEmployeeCommand, CreateUserDto>
    {
        private readonly IUserRepository _userRespository;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications,
            IUserRepository userRespository, IMapper mapper) : base(uow, bus, notifications)
        {
            _userRespository = userRespository;
            _mapper = mapper;
        }
        public async Task<CreateUserDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (! request.IsValid())
            {
                NotifyValidationErrors(request);
                return null;
            }

            var user = new User(request.Name, request.Surname, request.Email, request.Password, UserType.Employee.Id);
            user.AddUserCpf(request.Cpf);
            var userDto = _mapper.Map<CreateUserDto>(user);

            _userRespository.Add(user);

            if (await Commit())
            {
                return userDto;
            }

            return null;
        }
    }
}
