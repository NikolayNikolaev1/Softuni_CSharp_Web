namespace CameraBazaar.Data.Validations.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PhoneAttribute : ValidationAttribute
    {
        private const int DigitsMaxCount = 12;
        private const int DigitsMinCount = 10;
        private const string InvalidPhoneErrorMessage
            = "Phone must start with '+' sign followed by {0} to {1} digits.";

        public PhoneAttribute()
        {
            this.ErrorMessage = string.Format(
                InvalidPhoneErrorMessage, 
                DigitsMinCount, 
                DigitsMaxCount);
        }

        public override bool IsValid(object value)
        {
            string phone = value as string;

            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }

            string phoneDigits = phone.Substring(1);

            return phone.StartsWith("+")
                && phoneDigits.All(s => char.IsDigit(s))
                && phoneDigits.Length >= DigitsMinCount
                && phoneDigits.Length <= DigitsMaxCount;
        }
    }
}
