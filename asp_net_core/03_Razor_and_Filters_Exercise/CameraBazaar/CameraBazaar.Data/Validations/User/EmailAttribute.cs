namespace CameraBazaar.Data.Validations.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EmailAttribute : ValidationAttribute
    {
        private const string InvalidEmailErrorMessage = "Email is not valid.";
        private const string ValidEmailRegexPattern = @"^[^@\s]+@[^@\s\.]+\.[^@\.\s]+$";

        public EmailAttribute()
        {
            this.ErrorMessage = InvalidEmailErrorMessage;
        }

        public override bool IsValid(object value)
        {
            string email = value as string;
            Regex regex = new Regex(ValidEmailRegexPattern);

            return !string.IsNullOrEmpty(email)
                && regex.IsMatch(email);
        }
    }
}
