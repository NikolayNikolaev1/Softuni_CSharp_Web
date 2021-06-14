namespace GameStore.Application.Infrastructure
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using static Infrastructure.Constants;

    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = ErrorMessages.InvalidUserPassword;
        }

        public override bool IsValid(object value)
        {
            string password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Any(l => char.IsUpper(l))
                && password.Any(l => char.IsLower(l))
                && password.Any(l => char.IsDigit(l));
        }
    }
}
