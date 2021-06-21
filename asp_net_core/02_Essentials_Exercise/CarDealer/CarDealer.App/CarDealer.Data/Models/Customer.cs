namespace CarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
