namespace CameraBazaar.Data.Validations.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class UsernameAttribute : ValidationAttribute
    {
        private const int LettersMaxLength = 20;
        private const int LettersMinLength = 4;
        private string propertyName;

        public UsernameAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override bool IsValid(object value)
        {
            string username = value as string;

            if (string.IsNullOrEmpty(username))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidUsername, this.propertyName);
                return false;
            }

            if (username.Length < LettersMinLength ||
                username.Length > LettersMaxLength)
            {
                this.ErrorMessage = string.Format(
                    ErrorMessages.InvalidLegth,
                    this.propertyName,
                    LettersMinLength,
                    LettersMaxLength);

                return false;
            }

            if (username.Any(s => char.IsDigit(s)))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidSymbol, this.propertyName);
                return false;
            }

            return true;
        }

        private static class ErrorMessages
        {
            public const string InvalidLegth = "{0} must be between {1} and {2} symbols long.";
            public const string InvalidSymbol = "{0} must contain only letters. Casing does not matter.";
            public const string InvalidUsername = "{0} is required.";
        }
    }
}
