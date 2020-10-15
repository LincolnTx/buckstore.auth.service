using System;
using buckstore.auth.service.application.Commands;
using FluentValidation;

namespace buckstore.auth.service.application.Validations
{
    public class CreateUserValidations : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidations()
        {
            ValidateName();
            ValidateSurname();
            ValidateEmail();
            ValidatePassword();
            ValidateCpf();
        }
        
        // create validations

        protected void ValidateName()
        {
            RuleFor(createUser => createUser.Name)
                .NotEmpty().WithMessage("Campo nome obrigatório").WithErrorCode("001");
        }
        
        protected void ValidateSurname()
        {
            RuleFor(createUser => createUser.Surname)
                .NotEmpty().WithMessage("Campo sobrenome obrigatório").WithErrorCode("002");
        }
        
        protected void ValidateEmail()
        {
            RuleFor(createUser => createUser.Email)
                .NotEmpty().WithMessage("Campo email obrigatório").WithErrorCode("003")
                .EmailAddress().WithMessage("O campo precisa ser um email válido").WithErrorCode("004");
        }
        
        protected void ValidatePassword()
        {
            RuleFor(createUser => createUser.Password)
                .NotEmpty().WithMessage("Campo senha obrigatório").WithErrorCode("005");
            
            RuleFor(createUser => createUser).Custom((createUser, context) =>
            {
                if (!String.Equals(createUser.Password, createUser.ConfirmPassword))
                    context.AddFailure(nameof(createUser.Password), "As senhas devem ser iguais");
            });
        }
        
        protected void ValidateCpf()
        {
            RuleFor(createUser => createUser.Cpf)
                .NotEmpty().WithMessage("Campo cpf obrigatório").WithErrorCode("006")
                .MinimumLength(11).MaximumLength(11)
                .WithMessage("Cpf deve ter exatemente 11 caracteres").WithErrorCode("007");
        }
    }
}