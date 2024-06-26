﻿using System.ComponentModel.DataAnnotations;

namespace AspNetWebformSample.BusinessLayer.Models
{
    /// <summary>
    /// Represents a user profile with properties like ProfileId, Name, Address, Email, Mobile, and IsActive.
    /// </summary>
    public class UserProfile
    {
        [Display(Name = "Profile Id")]
        public int ProfileId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
    }
}