namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public int Id { get; set; }

        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
