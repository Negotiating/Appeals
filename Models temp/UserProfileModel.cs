using System;
using System.ComponentModel.DataAnnotations;

namespace Appeals.Models
{
    public class UserProfileModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string Surname { get; set; }

        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string House { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.UserProfile), ErrorMessageResourceName = "Required")]
        public string Apartment { get; set; }
    }
}
