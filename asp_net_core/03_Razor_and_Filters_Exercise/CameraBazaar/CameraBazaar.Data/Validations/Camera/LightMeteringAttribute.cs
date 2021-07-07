namespace CameraBazaar.Data.Validations.Camera
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class LightMeteringAttribute : ValidationAttribute
    {
        private const string InvalidErrorMessage = "Atleast one light metering type is rquired.";

        public LightMeteringAttribute()
        {
            this.ErrorMessage = InvalidErrorMessage;
        }

        public override bool IsValid(object value)
        {
            ICollection lightMeterings = value as ICollection;

            if (lightMeterings == null)
            {
                return false;
            }

            return lightMeterings.Count > 0;
        }
    }
}
