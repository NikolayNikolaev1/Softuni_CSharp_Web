namespace CarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {

        public int Id { get; set; }

        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        [Required]
        public string Make { get; set; }

        [Required]
        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        public string Model { get; set; }

        [Range(Constants.RangeMinValue, double.MaxValue)]
        // In kilometers.
        public double TravelledDistance { get; set; }

        public ICollection<CarPart> Parts { get; set; } = new List<CarPart>();

        public Sale Sale { get; set; }
    }
}
