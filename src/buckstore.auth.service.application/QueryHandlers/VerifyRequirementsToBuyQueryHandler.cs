using System;
using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.Queries;
using buckstore.auth.service.application.Queries.DTOs;
using buckstore.auth.service.application.Queries.ViewModels;
using buckstore.auth.service.domain.Exceptions;
using Dapper;
using MediatR;

namespace buckstore.auth.service.application.QueryHandlers
{
    public class VerifyRequirementsToBuyQueryHandler : QueryHandler, IRequestHandler<VerifyRequirementsToBuyQuery, UserRequirementsToBuyDto>
    {
        private readonly IMediator _bus;

        public VerifyRequirementsToBuyQueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<UserRequirementsToBuyDto> Handle(VerifyRequirementsToBuyQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = DbConnection)
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                const string sqlCommand = "SELECT u.cpf FROM  \"User\" u WHERE u.\"Id\" = @userId";

                try
                {
                    var data = await dbConnection.QueryFirstAsync<FindUserBuyRequirementsVW>(sqlCommand, new
                    {
                        userId = request.UserId
                    });

                    var cpfChecked = data.cpf != null;

                    return  new UserRequirementsToBuyDto
                    {
                        CpfChecked = cpfChecked,
                    };
                }
                catch (Exception e)
                {
                    await _bus.Publish(new ExceptionNotification("002",
                        "Usuário não encontrado",
                        "userID"), CancellationToken.None);

                    return default;
                }
            }
        }
    }
}
