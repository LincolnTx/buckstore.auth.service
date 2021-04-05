using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.application.Queries;

namespace buckstore.auth.service.api.v1.Controllers
{
    public class ValidationsController : BaseController
    {
        private readonly IMediator _bus;
        public ValidationsController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) 
            : base(notifications)
        {
            _bus = bus;
        }
        
        [HttpGet("buy-requirements")]
        public async Task<IActionResult> AbleToBuyValidation()
        {
            var userId = GetTokenClaim("id");
            var response = await _bus.Send(new VerifyRequirementsToBuyQuery(Guid.Parse(userId)));

            return Response(200, response);
        }
    }
}