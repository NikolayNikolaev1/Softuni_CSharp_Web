namespace CameraBazaar.Data.Validations.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        private const int SymbolsMinLength = 3;
        private string propertyName;

        public PasswordAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override bool IsValid(object value)
        {
            string password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidPassword, this.propertyName);
                return false;
            }

            if (password.Length < SymbolsMinLength)
            {
                this.ErrorMessage = string.Format(
                    ErrorMessages.InvalidLength,
                    this.propertyName,
                    SymbolsMinLength);

                return false;
            }

            if (password.Any(s => char.IsUpper(s)))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidSymbol, this.propertyName);
                return false;
            }

            return true;
        }

        private static class ErrorMessages
        {
            public const string InvalidLength = "{0} must be at least {1} symbols long.";
            public const string InvalidPassword = "{0} is required.";
            public const string InvalidSymbol = "{0} can contain only lowercase letters and digits";
        }
    }
}
