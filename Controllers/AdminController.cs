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

//Import tools needed for user login, roles, database, and web pages
using Microsoft.AspNetCore.Authorization; //Allows access control based on user roles
using Microsoft.AspNetCore.Identity; //Handles users and roles
using Microsoft.AspNetCore.Mvc; //Used to create and manage web pages
using Microsoft.EntityFrameworkCore; //Allows database queries
using ST10252746_PROG7311_POE_PART_2.Data;
using ST10252746_PROG7311_POE_PART_2.Models;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; //Used for filtering and selecting from data
using System.Threading.Tasks; 

namespace ST10252746_PROG7311_POE_PART_2.Controllers
{
    //Only Admin users are allowed to use this controller
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //Used to access the database
        private readonly ApplicationDbContext _context;

        //Used to manage users
        private readonly UserManager<IdentityUser> _userManager;

        //Used to manage user roles
        private readonly RoleManager<IdentityRole> _roleManager;

        //Constructor to set up the controller with the database, user, and role managers
        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Shows a list of all users who are employees
        public async Task<IActionResult> Employees()
        {
            var employeeRoleId = await _roleManager.FindByNameAsync("Employee");

            if (employeeRoleId == null)
                return View(new List<ApplicationUser>());

            var employeesQuery = from user in _context.ApplicationUsers
                                 join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                 where userRole.RoleId == employeeRoleId.Id
                                 select user;

            var employees = await employeesQuery.ToListAsync();
            return View(employees);
        }

        //Shows a form to add a new employee
        public IActionResult CreateEmployee()
        {
            return View();
        }

        //Handles the form when a new employee is submitted
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(RegisterEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return RedirectToAction(nameof(Employees));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        //Shows a confirmation page to delete an employee
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.ApplicationUsers.FindAsync(id);

            if (user == null)
                return NotFound();

            var isInRole = await _userManager.IsInRoleAsync(user, "Employee");
            if (!isInRole)
                return NotFound();

            return View(user);
        }

        //Deletes an employee after confirmation
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployeeConfirmed(string id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);

            if (user == null)
                return NotFound();

            var isInRole = await _userManager.IsInRoleAsync(user, "Employee");
            if (!isInRole)
                return NotFound();

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Employees));
        }
    }
}
