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

using Microsoft.AspNetCore.Mvc;
using ST10252746_PROG7311_POE_PART_2.Models; 
using System.Diagnostics; // Provides diagnostic info such as the current request's activity ID

namespace ST10252746_PROG7311_POE_PART_2.Controllers
{
    public class HomeController : Controller
    {
        //Used to log information, errors, etc
        private readonly ILogger<HomeController> _logger; 

        public HomeController(ILogger<HomeController> logger)
        {
            //Initializes the logger when the controller is created
            _logger = logger; 
        }

        public IActionResult Index()
        {
            //Returns the main page view
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //Creates an ErrorViewModel with a unique request ID and shows the error page
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}