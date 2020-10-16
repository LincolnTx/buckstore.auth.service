using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace buckstore.auth.service.api.v1.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IMediator _mediator;
        public  RegisterController (INotificationHandler<ExceptionNotification> notifications, 
            IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
           var userCreated = await _mediator.Send(createUserCommand);

            return Response(200, userCreated);
        }
    }
}