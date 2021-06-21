namespace ByTheCake.Models.ViewModels
{
    using System;

    public class ProfileViewModel
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int TotalOrders { get; set; }
    }
}
