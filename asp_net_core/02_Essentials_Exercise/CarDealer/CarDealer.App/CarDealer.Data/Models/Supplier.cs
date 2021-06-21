namespace CarDealer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public int Id { get; set; }

        [MaxLength(Constants.StringMaxLength)]
        [MinLength(Constants.StringMinlength)]
        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }
    }
}
