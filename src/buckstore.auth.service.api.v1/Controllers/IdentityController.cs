using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
           var userCreated = await _mediator.Send(createUserCommand);

            return Response(200, userCreated);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserCommand loginUserCommand)
        {
            var loginUserInfo = await _mediator.Send(loginUserCommand);
            
            return Response(200, new { });
        }
    }
}