namespace CarDealer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Part
    {
        public int Id { get; set; }

        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        [Required]
        public string Name { get; set; }

        [Range(Constants.RangeMinValue, Constants.RangeMaxValue)]
        public decimal Price { get; set; }

        [Range(Constants.RangeMinValue, Constants.RangeMaxValue)]
        public int Quantity { get; set; }
    }
}
