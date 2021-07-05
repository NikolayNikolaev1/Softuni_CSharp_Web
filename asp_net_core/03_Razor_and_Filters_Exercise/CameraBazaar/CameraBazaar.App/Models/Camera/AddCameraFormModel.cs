namespace CameraBazaar.App.Models.Camera
{
    using Data.Enums;
    using Data.Validations.Camera;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.Constants.ErrorMessages.Camera;
    using static Data.Common.Constants.Properties.Camera;

    public class AddCameraFormModel
    {
        public MakeType Make { get; set; }

        [Model(nameof(Model))]
        public string Model { get; set; }

        [Range(PriceMinValue, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(MinShutterSpeedMinValue, MinShutterSpeedMaxValue)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(MaxShutterSPeedMinValue, MaxShutterSpeedMaxValue)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinIsoType MinIso { get; set; }

        [Display(Name = "Max ISO")]
        [MaxIso(nameof(MaxIso))]
        public int MaxIso { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [MaxLength(VideoResolutionMaxLength)]
        [Required]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public ICollection<LightMeteringType> LightMetering { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Url]
        [Required]
        public string ImageUrl { get; set; }
    }
}
