namespace SocialNetwork.Validations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        private readonly char[] RequiredSymbols = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

        public PasswordAttribute()
        {
            this.ErrorMessage = "Password is not valid.";
        }

        public override bool IsValid(object value)
        {
            string password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Any(s => char.IsLower(s)
                || char.IsUpper(s)
                || char.IsDigit(s)
                || this.RequiredSymbols.Contains(s));

        }
    }
}
