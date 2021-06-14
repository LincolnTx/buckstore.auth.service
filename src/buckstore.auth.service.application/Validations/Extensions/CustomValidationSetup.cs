using FluentValidation;

namespace buckstore.auth.service.application.Validations.Extensions
{
    public static class CustomValidationSetup
    {
        public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CpfValidator(11, "O CPF informado é inválido!"));
        }
    }
}
