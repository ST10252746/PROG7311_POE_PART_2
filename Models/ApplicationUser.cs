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
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ST10252746_PROG7311_POE_PART_2.Models
{
    //This class adds extra user information to the default IdentityUser used by ASP.NET Core Identity
    public class ApplicationUser : IdentityUser
    {
        //Stores the user's first name and is required when registering
        [Required]
        public string Firstname { get; set; }

        //Stores the user's last name and is required when registering
        [Required]
        public string Lastname { get; set; }

        //Stores the user's location and shows as "Location" in forms
        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}
