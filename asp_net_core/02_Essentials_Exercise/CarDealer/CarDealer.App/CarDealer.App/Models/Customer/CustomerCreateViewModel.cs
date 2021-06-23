namespace CarDealer.Services.Models.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerCreateViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
