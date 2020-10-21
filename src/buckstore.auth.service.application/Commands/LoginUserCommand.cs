using buckstore.auth.service.application.Validations;
using MediatR;

namespace buckstore.auth.service.application.Commands
{
    public class LoginUserCommand :  Command, IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
       
        public override bool IsValid()
        {
            ValidationResult = new LoginUserValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}