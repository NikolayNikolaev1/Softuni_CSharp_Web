namespace CarDealer.App.Models.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerEditViewModel
    {
        [MaxLength(50)]
        [Required()]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
