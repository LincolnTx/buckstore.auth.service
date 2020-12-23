using MediatR;

namespace buckstore.auth.service.application.Commands
{
    public class FacebookLoginCommand : Command,  IRequest<LoginUserDto>
    {
        public string AccessToken { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
