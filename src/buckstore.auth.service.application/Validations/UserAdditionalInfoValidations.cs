using buckstore.auth.service.application.Commands;
using buckstore.auth.service.application.Validations.Extensions;
using FluentValidation;

namespace buckstore.auth.service.application.Validations
{
    public class UserAdditionalInfoValidations : AbstractValidator<UserAdditionalInfoCommand>
    {
        public UserAdditionalInfoValidations()
        {
            ValidateCpf();
            ValidateCredCard();
        }

        private void ValidateCpf()
        {
            RuleFor(command => command.Cpf)
                .NotEmpty()
                .WithMessage("O campo cpf é obrigatório!")
                .IsValidCpf()
                .WithMessage("o cpf informado esta em formato inválido!")
                .WithErrorCode("012");
        }

        private void ValidateCredCard()
        {
            //RuleFor(command => command.CredCard);
        }
    }
}
