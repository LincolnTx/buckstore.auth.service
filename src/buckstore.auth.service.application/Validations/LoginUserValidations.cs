using System.Data;
using buckstore.auth.service.application.Commands;
using FluentValidation;

namespace buckstore.auth.service.application.Validations
{
    public class LoginUserValidations : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidations()
        {
            ValidateEmail();
            ValidatePassword();
        }

        protected void ValidateEmail()
        {
            RuleFor(login => login.Email)
                .NotEmpty().WithMessage("Campo email obrigatório").WithErrorCode("009")
                .EmailAddress().WithMessage("O campo precisa ser um email válido").WithErrorCode("010");
        }

        protected void ValidatePassword()
        {
            RuleFor(login => login.Password)
                .NotEmpty().WithMessage("Campo senha é obrigatório").WithErrorCode("003")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caratcetres").WithErrorCode("011");
        }
    }
}