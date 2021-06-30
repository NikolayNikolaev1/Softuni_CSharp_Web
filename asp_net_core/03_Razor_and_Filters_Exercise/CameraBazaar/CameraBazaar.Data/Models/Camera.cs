namespace CameraBazaar.Data.Models
{
    using Enums;
    using System.ComponentModel.DataAnnotations;
    using Validations.Camera;

    public class Camera
    {
        public int Id { get; set; }

        public MakeType Make { get; set; }

        [Model(nameof(Model))]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        // Seconds
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        // Fraction of a second
        public int MaxShutterSpeed { get; set; }

        [MinIso]
        public int MinIso { get; set; }

        [Display(Name = "Max ISO")]
        [MaxIso(nameof(MaxIso))]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        public string VideoResolution { get; set; }

        public LightMeteringType LightMetering { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
