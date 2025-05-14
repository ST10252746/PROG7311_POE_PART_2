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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10252746_PROG7311_POE_PART_2.Data;
using ST10252746_PROG7311_POE_PART_2.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ST10252746_PROG7311_POE_PART_2.Controllers
{
    //Only users with the Farmer role can access this controller
    
    public class FarmerController : Controller
    {
        //Services that help interact with the database, users, and file storage
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<IdentityUser> _signInManager;

        //Constructor that sets up the services when the controller is used
        public FarmerController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        //Shows all products that belong to the current farmer
        public async Task<IActionResult> Products()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _context.Products
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View(products);
        }

        //Shows the form to add a new product
        public IActionResult CreateProduct()
        {
            return View();
        }

        //Handles the form submission when a farmer adds a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            try
            {
                ModelState.Remove("UserId");
                ModelState.Remove("User");
                ModelState.Remove("ImageUrlPath");

                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    product.ImageUrlPath = "/images/products/" + uniqueFileName;
                }
                else
                {
                    product.ImageUrlPath = "/images/products/default-product.jpg";
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product created successfully";
                return RedirectToAction(nameof(Products));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating product: {ex.Message}");
                return View(product);
            }
        }

        //Shows the form to update an existing product
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Products
                .Where(p => p.ProductId == id && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return View(product);
        }

        //Handles the form submission to update a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product, Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            if (id != product.ProductId)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.UserId != userId)
                return Unauthorized();

            ModelState.Remove("User");
            ModelState.Remove("imageFile");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Products.FindAsync(id);
                    if (existingProduct == null)
                        return NotFound();

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Category = product.Category;
                    existingProduct.ProductionDate = product.ProductionDate;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        if (!string.IsNullOrEmpty(existingProduct.ImageUrlPath) &&
                            !existingProduct.ImageUrlPath.EndsWith("default-product.jpg"))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                existingProduct.ImageUrlPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                            if (System.IO.File.Exists(oldImagePath))
                                System.IO.File.Delete(oldImagePath);
                        }

                        existingProduct.ImageUrlPath = "/images/products/" + uniqueFileName;
                    }

                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Product updated successfully";
                    return RedirectToAction(nameof(Products));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                        return NotFound();
                    else
                        throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error updating product: {ex.Message}");
                }
            }

            return View(product);
        }

        //Shows a confirmation page before deleting a product
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id && m.UserId == userId);

            if (product == null)
                return NotFound();

            return View(product);
        }

        //Handles the actual deletion of the product
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id && m.UserId == userId);

            if (product == null)
                return NotFound();

            if (!string.IsNullOrEmpty(product.ImageUrlPath))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                    product.ImageUrlPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Products));
        }

        //Displays the profile of the logged-in farmer
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.ApplicationUsers.FindAsync(userId);

            if (user == null)
                return NotFound();

            if (TempData["PasswordChangeSuccess"] != null)
                ViewBag.PasswordChangeSuccess = TempData["PasswordChangeSuccess"].ToString();
            if (TempData["PasswordChangeError"] != null)
                ViewBag.PasswordChangeError = TempData["PasswordChangeError"].ToString();

            return View(user);
        }

        //Handles profile information updates
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ApplicationUser user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _context.ApplicationUsers.FindAsync(userId);

            if (currentUser == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                currentUser.Firstname = user.Firstname;
                currentUser.Lastname = user.Lastname;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Location = user.Location;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Profile));
            }

            return View("Profile", user);
        }

        //Checks if the product exists in the database
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        //Handles password updates for the farmer account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                TempData["PasswordChangeError"] = "All fields are required";
                return RedirectToAction(nameof(Profile));
            }

            if (newPassword != confirmPassword)
            {
                TempData["PasswordChangeError"] = "The new password and confirmation password do not match";
                return RedirectToAction(nameof(Profile));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                TempData["PasswordChangeError"] = $"Failed to update password: {errors}";
                return RedirectToAction(nameof(Profile));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            TempData["PasswordChangeSuccess"] = "Your password has been updated successfully";
            return RedirectToAction(nameof(Profile));
        }
    }
}
