namespace CameraBazaar.Data.Common
{
    public static class Constants
    {

        public static class ErrorMessages
        {
            public const string EmptyField = "Field cannot be empty.";

            public static class Camera
            {
                public const string InvalidDescription = "Descripiton must have details for the camera with no more than 6000 symbols.";
                public const string InvalidMaxShutterSpeed = "Max Shutter Speed must be a number in range 2000 to 8000 (fraction of a second).";
                public const string InvalidMinShutterSpeed = "Min Shutter Speed must be a number in range 1 to 30 (seconds).";
                public const string InvalidModel = "Model can contain only uppercase letters, digits and dash ('-').";
                public const string InvalidPrice = "Price msut be a floating point number with precision to 2 digits after floating point.";
                public const string InvalidQuantity = "Quantity must be a number in range 0 – 100.";
                public const string InvalidVideoResolution = "Video Resolution msut be described with text no longer than 15 symbols.";
            }
        }

        public static class Properties
        {
            public static class Camera
            {
                public const int DescriptionMaxLength = 6000;
                public const int MaxShutterSpeedMaxValue = 8000;
                public const int MaxShutterSPeedMinValue = 2000;
                public const int MinShutterSpeedMaxValue = 30;
                public const int MinShutterSpeedMinValue = 1;
                public const double PriceMinValue = 0;
                public const int QuantityMaxValue = 100;
                public const int QuantityMinValue = 0;
                public const int VideoResolutionMaxLength = 15;
            }
        }
    }
}
