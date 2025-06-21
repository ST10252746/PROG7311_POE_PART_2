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

Author: Microsoft Learn
Link: https://learn.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity
Date Accessed: 02 May 2025

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

//Import tools needed for login, user roles, and web pages
using Microsoft.AspNetCore.Authorization; //Used to control access to pages based on user roles
using Microsoft.AspNetCore.Identity; //Used to manage users and their roles
using Microsoft.AspNetCore.Mvc; //Used to build web pages and handle user actions

namespace ST10252746_PROG7311_POE_PART_2.Controllers
{
    //Only users with the "Admin" role can access this controller
    
    [Authorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        //Used to manage user roles in the system
        private readonly RoleManager<IdentityRole> _roleManager;

        //Sets up role management when this controller is created
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Shows a list of all roles in the system
        public IActionResult Index()
        {
            var roles = _roleManager.Roles; //Get all the roles
            return View(roles); //Show the list in a web page
        }

        //Shows the form for creating a new role
        [HttpGet]
        public IActionResult Create()
        {
            return View(); //Show the create role form
        }

        //Handles the form when the admin submits a new role
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //Check if the role already exists
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                //If it doesn’t exist, create the new role
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }

            return RedirectToAction("Index"); //After saving, go back to the list
        }
    }
}
