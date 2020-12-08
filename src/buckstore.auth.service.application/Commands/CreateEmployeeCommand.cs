using buckstore.auth.service.application.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace buckstore.auth.service.application.Commands
{
    public class CreateEmployeeCommand : Command, IRequest<CreateUserDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateEmployeeValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
