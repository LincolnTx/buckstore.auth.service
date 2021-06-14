﻿using System;
using System.Net;
using MediatR;
using System.Threading.Tasks;
using buckstore.auth.service.application.Commands;
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

        [HttpPost("provide-additional-info")]
        public async Task<IActionResult> ProvideBuyAdditionalInfo([FromBody] UserAdditionalInfoCommand userInfoCommand)
        {
            var userId = GetTokenClaim("id");
            userInfoCommand.UserId = Guid.Parse(userId);
            var response = await _bus.Send(userInfoCommand);

            return Response(204, response);
        }
    }
}