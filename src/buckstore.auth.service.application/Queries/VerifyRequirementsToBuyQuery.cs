using System;
using buckstore.auth.service.application.Queries.DTOs;
using MediatR;

namespace buckstore.auth.service.application.Queries
{
    public class VerifyRequirementsToBuyQuery : IRequest<UserRequirementsToBuyDto>
    {
        public Guid UserId { get; set; }

        public VerifyRequirementsToBuyQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}