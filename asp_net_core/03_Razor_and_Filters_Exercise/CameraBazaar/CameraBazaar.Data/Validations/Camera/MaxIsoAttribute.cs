namespace CameraBazaar.Data.Validations.Camera
{
    using System.ComponentModel.DataAnnotations;

    public class MaxIsoAttribute : ValidationAttribute
    {
        private const int DividableNumber = 100;
        private const int MaxIsoMaxValue = 409600;
        private const int MaxIsoMinValue = 200;
        private string propertyName;

        public MaxIsoAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override bool IsValid(object value)
        {
            int iso = (int)value;

            if (iso < MaxIsoMinValue || iso > MaxIsoMaxValue)
            {
                this.ErrorMessage = string.Format(
                    ErrorMessages.InvalidRange,
                    this.propertyName,
                    MaxIsoMinValue,
                    MaxIsoMaxValue);

                return false;
            }

            if (iso % DividableNumber != 0)
            {
                this.ErrorMessage = string.Format(ErrorMessages.InvalidNumber,
                    this.propertyName,
                    DividableNumber);

                return false;
            }

            return true;
        }

        private static class ErrorMessages
        {
            public const string InvalidRange = "{0} must be an integer number in range {1} to {2}";
            public const string InvalidNumber = "{0} must be dividable by {1}";
        }
    }
}
