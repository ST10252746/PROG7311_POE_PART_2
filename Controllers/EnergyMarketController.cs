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

namespace ST10251759_PROG7313_POE_PART_2.Controllers
{
    public class EnergyMarketController : Controller
    { //Defines an action method named 'Index'
        public IActionResult Index()
        { //Calls the 'View()' method to return a ViewResult object
            return View();
        }
    }
}
