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
    //Used to collect and validate input when an admin registers a new employee
    public class RegisterEmployeeViewModel
    {
        //Stores the employee's first name
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        //Stores the employee's last name
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        //Stores the employee's email address and checks that the format is valid
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Stores the password chosen by the employee and ensures it meets length rules
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //Stores the confirmation of the password and checks it matches the one above
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
