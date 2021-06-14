using System;
using buckstore.auth.service.application.Validations;
using MediatR;

namespace buckstore.auth.service.application.Commands
{
    public class UserAdditionalInfoCommand : Command, IRequest
    {
        public Guid UserId { get; set; }
        public string Cpf { get; set; }
        public string CredCard { get; set; }
        
        public override bool IsValid()
        {
            ValidationResult = new UserAdditionalInfoValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}