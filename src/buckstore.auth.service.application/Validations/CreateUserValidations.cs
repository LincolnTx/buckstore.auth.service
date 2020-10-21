﻿using System;
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
                .NotEmpty().WithMessage("Campo senha obrigatório").WithErrorCode("005")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caratcetres").WithErrorCode("006");
            
            RuleFor(createUser => createUser).Custom((createUser, context) =>
            {
                if (!String.Equals(createUser.Password, createUser.ConfirmPassword))
                    context.AddFailure(nameof(createUser.Password), "As senhas devem ser iguais");
            });
        }
        
        protected void ValidateCpf()
        {
            RuleFor(createUser => createUser.Cpf)
                .NotEmpty().WithMessage("Campo cpf obrigatório").WithErrorCode("007")
                .MinimumLength(11).MaximumLength(11)
                .WithMessage("Cpf deve ter exatemente 11 caracteres").WithErrorCode("008");
        }
    }
}