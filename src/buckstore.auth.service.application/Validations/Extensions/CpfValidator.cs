using System.Linq;
using System.Collections.Generic;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace buckstore.auth.service.application.Validations.Extensions
{
    public class CpfValidator: PropertyValidator
    {
        private readonly int _validLength;
        protected IEnumerable<int> FirstMultiplierCollection => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected IEnumerable<int> SecondMultiplierCollection => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        internal CpfValidator(int validLength, string errorMessage) : base(errorMessage)
        {
            _validLength = validLength;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string ?? string.Empty;
            value = Regex.Replace(value, "[^a-zA-Z0-9]", "");

            if (IsValidLength(value) ||
                AllDigitsAreEqual(value) ||
                context.PropertyValue == null) return false;

            var cpf = value.Select(x => (int)char.GetNumericValue(x)).ToArray();
            var digits = GetDigits(cpf);

            return value.EndsWith(digits);
        }

        private static bool AllDigitsAreEqual(string value) => value.All(valueChar => valueChar == value.FirstOrDefault());

        private bool IsValidLength(string value) => !string.IsNullOrWhiteSpace(value) && value.Length != _validLength;

        private string GetDigits(IReadOnlyList<int> cpf)
        {
            var first = CalculateValue(FirstMultiplierCollection, cpf);
            var second = CalculateValue(SecondMultiplierCollection, cpf);

            return $"{CalculateDigit(first)}{CalculateDigit(second)}";
        }

        private static int CalculateValue(IEnumerable<int> weight, IReadOnlyList<int> numbers)
        {
            return weight.Select((t, i) => t * numbers[i]).Sum();
        }

        private static int CalculateDigit(int sum)
        {
            var modResult = (sum % 11);
            return modResult < 2 ? 0 : 11 - modResult;
        }
    }
}