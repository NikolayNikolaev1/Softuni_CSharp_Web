namespace CameraBazaar.Data.Common
{
    public static class Constants
    {

        public static class ErrorMessages
        {
            public static class Camera
            {

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
