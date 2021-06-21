namespace ByTheCake.Models.ViewModels
{
    using System;

    public class OrderListingViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalSum { get; set; }
    }
}
