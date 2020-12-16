using buckstore.auth.service.application.Commands;
using FluentValidation;


namespace buckstore.auth.service.application.Validations
{
    public class ChangeUserPasswordValidations : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidations()
        {
            ValidatePassword();
            ValidateNewPassword();
        }

        protected void ValidatePassword()
        {
            RuleFor(createUser => createUser.CurrentPassword)
                .NotEmpty().WithMessage("Campo senha obrigatório").WithErrorCode("012")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caratcetres").WithErrorCode("013");
        }

        protected void ValidateNewPassword()
        {
            RuleFor(createUser => createUser).Custom((createUser, context) =>
            {
                if (!string.Equals(createUser.NewPassword, createUser.ConfirmNewPassword))
                    context.AddFailure(nameof(createUser.NewPassword), "As senhas devem ser iguais");
            });
        }
    }
}
