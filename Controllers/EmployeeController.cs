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

//Import tools needed to control who can access pages, manage users and roles, and work with the database
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10252746_PROG7311_POE_PART_2.Data;
using ST10252746_PROG7311_POE_PART_2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ST10252746_PROG7311_POE_PART_2.Controllers
{
    //Only users who are Employees can use this controller
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        //Connect to the database and manage users and roles
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //Constructor sets up the controller with needed tools
        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Shows a list of all users who are farmers
        public async Task<IActionResult> Farmers()
        {
            var farmerRoleId = await _roleManager.FindByNameAsync("Farmer");

            if (farmerRoleId == null)
                return View(new List<ApplicationUser>());

            var farmersQuery = from user in _context.ApplicationUsers
                               join userRole in _context.UserRoles on user.Id equals userRole.UserId
                               where userRole.RoleId == farmerRoleId.Id
                               select user;

            var farmers = await farmersQuery.ToListAsync();
            return View(farmers);
        }

        //Shows a form to create a new farmer account
        public IActionResult CreateFarmer()
        {
            return View();
        }

        //Handles the form after it is submitted to create a new farmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFarmer(RegisterFarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    PhoneNumber = model.PhoneNumber,
                    Location = model.Location
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Farmer");
                    return RedirectToAction(nameof(Farmers));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        //Shows a confirmation page to delete a farmer
        public async Task<IActionResult> DeleteFarmer(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            var isInRole = await _userManager.IsInRoleAsync(user, "Farmer");
            if (!isInRole)
                return NotFound();

            return View(user);
        }

        //Handles the deletion of a farmer after confirmation
        [HttpPost, ActionName("DeleteFarmer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFarmerConfirmed(string id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            var isInRole = await _userManager.IsInRoleAsync(user, "Farmer");
            if (!isInRole)
                return NotFound();

            var products = await _context.Products.Where(p => p.UserId == id).ToListAsync();
            _context.Products.RemoveRange(products);

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Farmers));
        }

        //Shows a list of products and lets users filter by date, category, farmer, or search
        public async Task<IActionResult> Products(DateTime? fromDate, DateTime? toDate, string category, string farmerId, string searchString)
        {
            var productsQuery = _context.Products.Include(p => p.User).AsQueryable();

            if (fromDate.HasValue)
                productsQuery = productsQuery.Where(p => p.ProductionDate >= fromDate.Value);

            if (toDate.HasValue)
                productsQuery = productsQuery.Where(p => p.ProductionDate <= toDate.Value);

            if (!string.IsNullOrEmpty(category))
                productsQuery = productsQuery.Where(p => p.Category == category);

            if (!string.IsNullOrEmpty(farmerId))
                productsQuery = productsQuery.Where(p => p.UserId == farmerId);

            var farmerRoleId = _context.Roles.FirstOrDefault(r => r.Name == "Farmer")?.Id;
            var farmerIds = _context.UserRoles
                .Where(ur => ur.RoleId == farmerRoleId)
                .Select(ur => ur.UserId)
                .ToList();

            var farmers = await _context.ApplicationUsers
                .Where(u => farmerIds.Contains(u.Id))
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(searchString) ||
                    p.Description.Contains(searchString) ||
                    p.User.Firstname.Contains(searchString) ||
                    p.User.Lastname.Contains(searchString) ||
                    p.User.Email.Contains(searchString));
            }

            var products = await productsQuery.ToListAsync();

            ViewBag.Categories = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.Category = category;
            ViewBag.FarmerId = farmerId;
            ViewBag.Farmers = farmers;
            ViewBag.SearchString = searchString;

            return View(products);
        }

        //Shows more details about a selected product
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
