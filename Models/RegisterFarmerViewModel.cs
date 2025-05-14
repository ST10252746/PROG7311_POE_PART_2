/*
------------------------------------------------------------------------------
                        CODE ATTRIBUTION:
------------------------------------------------------------------------------
 
Author: Microsoft Learn
Link:https://learn.microsoft.com/enus/aspnet/core/security/authentication/?view=aspnetcore-8.0
Date Accessed: 01 May 2025

Author: Microsoft Learn
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-8.0
Date Accessed: 04 May 2025

Author: Daryl Young
Link: https://vocal.media/geeks/how-to-connect-sql-server-database-in-asp-net-core-mvc 
Date Accessed: 03 May 2025


Template for Styling
Author: ThemeWagon
Link: https://themewagon.com/themes/free-bootstrap-5-html5-renewable-energy-website-template-solartec/
Date Accessed: 03 May 2025

Author: w3schools
Link: https://www.w3schools.com/css/
Date Accessed: 08 May 2025

------------------------------------------------------------------------------
                        CODE ATTRIBUTION
------------------------------------------------------------------------------
*/

using System.ComponentModel.DataAnnotations;

namespace ST10252746_PROG7311_POE_PART_2.Models
{
    //Used to capture information when a new farmer registers

    public class RegisterFarmerViewModel
    {
        //Farmer’s first name (must be filled in)
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        //Farmer’s last name (must be filled in)
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        //Farmer’s email address (must be valid format)
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Farmer’s phone number (must start with 0 and have 10 digits)
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be 10 digits and start with 0.")]
        public string PhoneNumber { get; set; }

        //Location of the farmer (for deliveries or farm info)
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        //Type of farm (e.g., Crop, Livestock, Mixed)
        [Required(ErrorMessage = "Farm type is required")]
        [Display(Name = "Farm Type")]
        public string FarmType { get; set; }

        //Password for login (must be between 6 and 100 characters)
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //Confirm password (must match the password above)
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
