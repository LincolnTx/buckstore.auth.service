using buckstore.auth.service.application.Validations;
using MediatR;

namespace buckstore.auth.service.application.Commands
{
    public class ChangeUserPasswordCommand : Command, IRequest<bool>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Email { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ChangeUserPasswordValidations().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
