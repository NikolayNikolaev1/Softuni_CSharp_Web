namespace CameraBazaar.Data.Validations.Camera
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class ModelAttribute : ValidationAttribute
    {
        private const string ValidModelRegexPattern = @"^[A-Z0-9-]+$";
        private string propertyName;

        public ModelAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override bool IsValid(object value)
        {
            string model = value as string;

            if (string.IsNullOrEmpty(model))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidModel, this.propertyName);
                return false;
            }

            Regex regex = new Regex(ValidModelRegexPattern);

            if (!regex.IsMatch(model))
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidSymbol, this.propertyName);
                return false;
            }

            return true;
        }

        private static class ErrorMessages
        {
            public const string InvalidModel = "{0} cannot be empty.";
            public const string InvalidSymbol = "{0} can contain only uppercase letters, digits and dash ('-').";
        }
    }
}
