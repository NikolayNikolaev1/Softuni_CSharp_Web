namespace CameraBazaar.Data.Validations.Camera
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MinIsoAttribute : ValidationAttribute
    {
        private readonly int[] ValidValues = { 50, 100 };

        public MinIsoAttribute()
        {
            this.ErrorMessage = "Min ISO can be 50 or 100"; // TODO: Use reflection..
        }

        public override bool IsValid(object value)
        {
            int iso = int.Parse(value as string);

            if (!ValidValues.Contains(iso))
            {
                return false;
            }

            return true;
        }
    }
}
