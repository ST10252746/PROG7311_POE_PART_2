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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10252746_PROG7311_POE_PART_2.Models
{
    //Used to represent a product in the application, created or managed by a farmer or employee
    //This model links to a user via a foreign key and includes validation attributes to enforce required fields
    public class Product
    {
        //Primary key for the Product table
        //Uniquely identifies each product record
        public int ProductId { get; set; }

        //Stores the name of the product
        //This field is required to ensure the product has a name
        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        //Holds a description of the product's characteristics or purpose
        //This field is required for clear understanding and categorization
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Indicates the category or type of product (e.g., vegetables, dairy)
        //Required for proper filtering or reporting
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        //Records the date when the product was produced or harvested
        //This field is required for traceability and tracking purposes
        [Required(ErrorMessage = "Production date is required")]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        //Stores the file path or URL of the product's image
        //This field is optional for users who may choose not to upload an image
        [Display(Name = "Image")]
        public string ImageUrlPath { get; set; }

        //Stores the ID of the user (farmer or employee) who created the product
        //This property is set programmatically based on the logged-in user's identity
        public string UserId { get; set; }

        //Associates the product with a specific application user via a foreign key
        //Marked as virtual to support lazy loading if available
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        
    }
}