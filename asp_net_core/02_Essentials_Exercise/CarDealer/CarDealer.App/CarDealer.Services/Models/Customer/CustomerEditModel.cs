namespace CarDealer.Services.Models.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerEditModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required()]
        public string Name { get; set; }

        [Required()]
        public DateTime BirthDate { get; set; }
    }
}
