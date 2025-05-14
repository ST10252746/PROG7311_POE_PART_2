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

using Microsoft.EntityFrameworkCore;                    
using ST10252746_PROG7311_POE_PART_2.Data;              
using Microsoft.AspNetCore.Identity;                   

namespace ST10252746_PROG7311_POE_PART_2
{
    //This is where the web application starts running.
    public class Program
    {
        public static void Main(string[] args)
        {
            //Set up everything needed to run the application
            var builder = WebApplication.CreateBuilder(args);

            //Add support for MVC structure (Model-View-Controller)
            builder.Services.AddControllersWithViews();

            //Connect the application to the SQL Server database using the settings from appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Set up user login, registration, and role management
            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Finalize setup and build the app
            var app = builder.Build();

            //If not in development mode, show a user-friendly error page and enforce secure connections
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Makes sure the site always uses HTTPS
            }

            //Redirect HTTP traffic to HTTPS
            app.UseHttpsRedirection();

            //Allow serving of files like CSS, JavaScript, and images
            app.UseStaticFiles();

            //Set up routing so the app knows how to handle different page requests
            app.UseRouting();

            //Enable user login features
            app.UseAuthentication();

            //Enable role-based access control
            app.UseAuthorization();

            //Map Razor Pages used for user-related screens (login, register, etc.)
            app.MapRazorPages();

            //Set the default page to Home/Index unless a specific page is requested
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Start the app and listen for web requests
            app.Run();
        }
    }
}