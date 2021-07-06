namespace CameraBazaar.Services.Models.Camera
{
    using Data.Enums;
    using Data.Validations.Camera;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.Constants;
    using static Data.Common.Constants.Properties.Camera;

    public class CameraFormServiceModel
    {
        public int Id { get; set; }

        public MakeType Make { get; set; }

        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = ErrorMessages.Camera.InvalidModel)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public string Model { get; set; }

        [Range(PriceMinValue, double.MaxValue, ErrorMessage = ErrorMessages.Camera.InvalidPrice)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public decimal Price { get; set; }

        [Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = ErrorMessages.Camera.InvalidQuantity)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(MinShutterSpeedMinValue, MinShutterSpeedMaxValue,
            ErrorMessage = ErrorMessages.Camera.InvalidMinShutterSpeed)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(MaxShutterSPeedMinValue, MaxShutterSpeedMaxValue,
            ErrorMessage = ErrorMessages.Camera.InvalidMaxShutterSpeed)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinIsoType MinIso { get; set; }

        [Display(Name = "Max ISO")]
        [MaxIso(nameof(MaxIso))]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public int MaxIso { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [MaxLength(VideoResolutionMaxLength, ErrorMessage = ErrorMessages.Camera.InvalidVideoResolution)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public ICollection<LightMeteringType> LightMetering { get; set; }

        [MaxLength(DescriptionMaxLength, ErrorMessage = ErrorMessages.Camera.InvalidDescription)]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = ErrorMessages.EmptyField)]
        [Url]
        public string ImageUrl { get; set; }
    }
}
