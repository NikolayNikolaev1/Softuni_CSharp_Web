using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public double Price { get; set; }

        public List<OrderItem> Orders { get; set; } = new List<OrderItem>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
