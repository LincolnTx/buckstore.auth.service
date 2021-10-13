using MediatR;
using System.Threading.Tasks;
using buckstore.auth.service.api.v1.Filters.AuthorizationFilters;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using buckstore.auth.service.api.v1.Dtos;

namespace buckstore.auth.service.api.v1.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        public  IdentityController (INotificationHandler<ExceptionNotification> notifications,
            IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
            createUserCommand.UserType = UserType.Customer.Id;
            var userCreated = await _mediator.Send(createUserCommand);

            return Response(Ok(new BaseResponseDto<CreateUserDto> { Success = true, Data = userCreated }));
        }

        [HttpPost("register-employee")]
        [Authorize(nameof(UserType.Admin))]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            var createEmployeeResponse = await _mediator.Send(createEmployeeCommand);

            return Response(Ok(new BaseResponseDto<CreateUserDto> { Success = true, Data = createEmployeeResponse }));
        }

        [HttpPost("register-admin")]
        //[Authorize(nameof(UserType.Admin))]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateUserCommand createAdminCommand)
        {
            createAdminCommand.UserType = UserType.Admin.Id;
            var createAdminResponse = await _mediator.Send(createAdminCommand);

            return Response(Ok(new BaseResponseDto<CreateUserDto> { Success = true, Data = createAdminResponse }));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserCommand loginUserCommand)
        {
            var loginUserInfo = await _mediator.Send(loginUserCommand);

            return Response(Ok(new BaseResponseDto<LoginUserDto> { Success = true, Data =  loginUserInfo }));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] ChangeUserPasswordCommand changePasswordCommand)
        {
            var changedPassword = await _mediator.Send(changePasswordCommand);

            return Response(Ok(new BaseResponseDto<bool> {Success =  true, Data =  changedPassword }));
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginCommand loginUserCommand)
        {
            var loginUserInfo = await _mediator.Send(loginUserCommand);

            return Response(Ok(new BaseResponseDto<LoginUserDto> {Success =  true, Data =  loginUserInfo }));
        }
    }
}
