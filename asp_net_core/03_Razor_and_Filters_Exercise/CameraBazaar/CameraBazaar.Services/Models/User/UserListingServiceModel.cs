using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar.Services.Models.User
{
    public class UserListingServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsRestrict { get; set; }
    }
}
