namespace CameraBazaar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public DateTime? LastLoginTime { get; set; }

        public bool IsRestrict { get; set; }

        public ICollection<Camera> Cameras { get; set; } = new List<Camera>();
    }
}
