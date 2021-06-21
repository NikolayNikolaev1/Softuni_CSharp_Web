namespace CarDealer.Data.Models
{
    using System;
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

        [Range(Constants.RangeMinValue, Constants.RangeMaxValue)]
        // In kilometers.
        public double TravelledDistance { get; set; } 
    }
}
