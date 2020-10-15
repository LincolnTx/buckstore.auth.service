using buckstore.auth.service.application.Validations;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using MediatR;

namespace buckstore.auth.service.application.Commands
{
    public class CreateUserCommand : Command, IRequest<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Cpf { get; set; }
       
        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}