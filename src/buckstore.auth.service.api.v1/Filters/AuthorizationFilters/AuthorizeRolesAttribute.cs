using buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace buckstore.auth.service.api.v1.Filters.AuthorizationFilters
{

    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string [] roles) : base(typeof(AuthorizeRolesFilter))
        {
            Arguments = new[] { roles };
        }
    }

    public class AuthorizeRolesFilter : Attribute, IAsyncResourceFilter
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;
        private readonly string[] _roles;

        public AuthorizeRolesFilter(IIdentityService identityService, string[] roles,ILogger<AuthorizeAttribute> logger)
        {
            _identityService = identityService;
            _logger = logger;
            _roles = roles;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            string token = !(String.IsNullOrEmpty(authHeader)) ? authHeader.Replace("Bearer ", "") : "";
            try
            {
                if (! _identityService.ValidateToken(token))
                {
                    _logger.LogWarning("Usuário passou um token inválido na rota {0}.", context.HttpContext.Request.Path);
                    context.Result = new UnauthorizedResult();
                    await context.Result.ExecuteResultAsync(context);
                    return;
                }

                var tokenClaims = _identityService.GetTokenClaims(token);
                var userRole = tokenClaims.FirstOrDefault(claim => claim.Type == "Role");

                var isRoleValid = _roles.Contains(userRole.Value);

                if (!isRoleValid)
                {
                    context.Result = new UnauthorizedResult();
                    await context.Result.ExecuteResultAsync(context);
                    _logger.LogWarning("Usuário passou um  token sem acesso na rota {0}.", context.HttpContext.Request.Path);
                }
                else
                {
                    await next();
                }
            }
            catch (ArgumentNullException)
            {
                context.Result = new UnauthorizedResult();
                _logger.LogWarning("Usuário não passou um token na rota {0}", context.HttpContext.Request.Path);
            }
        }
    }
}
